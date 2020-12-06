using Common.Api;
using Common.Events;
using Common.Models;
using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class DbNotifierService : IDbNotifier
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly object lockObj = new object();

        public DbNotifierService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async void NotifyFlightMoved(FlightEvent flightEvent)
        {
            Flight flight = flightEvent.Flight;
            if (flightEvent.IsFromControlTowerToFirstStation)
            {
                OpenFlightHistory(flight, flightEvent.StationTo);
            }
            else if (flightEvent.IsFromLastStationToEnd)
            {
                CloseFlightHistory(flight, flightEvent.StationFrom);
            }
            else
            {
                CloseFlightHistoryAndCreateNew(flight, flightEvent.StationFrom, flightEvent.StationTo);
            }
            lock (lockObj)
            {
                flight.StationId = flightEvent.StationTo?.Id;
            }

            using IServiceScope scope = serviceScopeFactory.CreateScope();
            IRepository<Flight> flightRepository = scope.ServiceProvider.GetRequiredService<IRepository<Flight>>();
            await flightRepository.UpdateAsync(flightEvent.Flight);
        }
        private void OpenFlightHistory(Flight flight, Station to)
        {
            PlaneStationHistory planeStationHistory = new PlaneStationHistory { StationId = to.Id, EnterStationTime = DateTime.Now };
            if (flight.FlightHistory is null)
            {
                flight.FlightHistory = new List<PlaneStationHistory>();
            }
            flight.FlightHistory.Add(planeStationHistory);
        }
        private void CloseFlightHistory(Flight flight, Station from)
        {
            PlaneStationHistory planeStationHistory = flight.FlightHistory.FirstOrDefault(fh => fh.StationId == from.Id && !fh.ExitStationTime.HasValue);
            if (planeStationHistory != null)
                planeStationHistory.ExitStationTime = DateTime.Now;
        }
        private void CloseFlightHistoryAndCreateNew(Flight flight, Station from, Station to)
        {
            CloseFlightHistory(flight, from);
            OpenFlightHistory(flight, to);
        }


    }
}
