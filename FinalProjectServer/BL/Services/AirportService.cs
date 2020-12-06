using Common.Api;
using Common.Events;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class AirportService : IAirportService
    {
        public ICollection<IStation> Stations { get; private set; }
        public IControlTowerService ControlTowerService { get; private set; }
        public bool IsInit { get; private set; }
        private readonly IAirportNotifierService airportNotifier;
        private readonly IDbNotifier dbNotifier;

        public AirportService(IAirportNotifierService airportNotifier, IDbNotifier dbNotifier)
        {
            this.airportNotifier = airportNotifier;
            this.dbNotifier = dbNotifier;
        }

        public AirportService()
        {
        }

        public void BuildRelation(ControlTower controlTower, ICollection<Station> stations)
        {
            if (Stations is null)
                Stations = new List<IStation>();
            var newStations = stations.Where(st => !Stations.Any(s => s.StationDto.Id == st.Id));
            if (CreateLogicalStations(newStations))
                BuildStationConnections();
            CreateLogicalControlTower(controlTower);
            BuildConnections(ControlTowerService);
        }
        public void FlightsLoaded()
        {
            IsInit = true;
        }
        public void PlaneArrived(Flight flight)
        {
            if (flight is null)
            {
                throw new ArgumentNullException(nameof(flight), "flight can not be null");
            }

            ControlTowerService.PlaneArrived(new Models.Plane(flight));
        }
        private void BuildStationConnections()
        {
            foreach (var item in Stations)
            {
                BuildConnections(item);
            }
        }
        private void BuildConnections(INextStation nextStation)
        {
            ICollection<IStation> NextLandingStations = nextStation.RelatedToStations.Where(st => st.Direction == DirectionEnum.Landing)
                .Join(Stations, rtns => rtns.StationToId, st => st.StationDto.Id, (rtns, st) => st).ToArray();

            ICollection<IStation> NextTakeoffStations = nextStation.RelatedToStations.Where(st => st.Direction == DirectionEnum.Takeoff)
                .Join(Stations, rtns => rtns.StationToId, st => st.StationDto.Id, (rtns, st) => st).ToList();

            nextStation.SetStations(NextLandingStations, NextTakeoffStations);
        }
        private void CreateLogicalControlTower(ControlTower controlTower)
        {
            ControlTowerService = new ControlTowerService(controlTower);
            ControlTowerService.FlightMoved += FlightChanged;
        }
        private bool CreateLogicalStations(IEnumerable<Station> stations)
        {
            bool isNew = false;
            foreach (var item in stations)
            {
                isNew = true;
                IStation station = new Models.StationLogic(item);
                station.FlightMoved += FlightChanged;
                Stations.Add(station);
            }
            return isNew;

        }
        private void FlightChanged(object sender, FlightEvent e)
        {
            airportNotifier.NotifyFlightMoved(e);
            dbNotifier.NotifyFlightMoved(e);
        }
    }
}