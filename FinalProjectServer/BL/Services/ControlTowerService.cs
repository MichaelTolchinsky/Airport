using BL.Models;
using Common.Api;
using Common.Events;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class ControlTowerService : IControlTowerService
    {
        public event EventHandler<FlightEvent> FlightMoved;
        public ControlTower ControlTower { get; private set; }
        public ICollection<IStation> LandStations { get; set; }
        public ICollection<IStation> TakeoffStations { get; set; }
        public Queue<Flight> WaitingLandingFlights { get; set; }
        public Queue<Flight> WaitingTakeoffFlights { get; set; }
        public IEnumerable<IRelatedToNextStation> RelatedToStations => ControlTower?.StartupStations;
        private readonly object LockObj = new object();

        public ControlTowerService(ControlTower controlTower)
        {
            ControlTower = controlTower ?? throw new ArgumentNullException("control tower can't be null");
            WaitingLandingFlights = new Queue<Flight>();
            WaitingTakeoffFlights= new Queue<Flight>();
        }
        public bool PlaneArrived(IPlane plane)
        {
            if (plane is null)
            {
                throw new ArgumentNullException(nameof(plane), "flight can't be null");
            }

            var flights = GetRelevantFlightQueue(plane.Flight.FlightDirection);
            if (flights.Count > 0)
            {
                AddFlightsToWaitingList(plane.Flight);
            }
            else
            {
                SendPlaneToRelevantStation(plane.Flight, false);
            }
            return true;
        }
        public void SetStations(ICollection<IStation> landStations, ICollection<IStation> takeoffStations)
        {
            UnregisterToStationEvents();
            LandStations = landStations;
            TakeoffStations = takeoffStations;
            RegisterToStationEvents();
        }
        private Queue<Flight> GetRelevantFlightQueue(DirectionEnum direction)
        {
            return direction == DirectionEnum.Landing ? WaitingLandingFlights : WaitingTakeoffFlights;
        }
        private ICollection<IStation> GetRelevantStations(DirectionEnum direction)
        {
            return direction == DirectionEnum.Landing ? LandStations : TakeoffStations;
        }
        private void AddFlightsToWaitingList(Flight flight)
        {
            var flights = GetRelevantFlightQueue(flight.FlightDirection);
            flights.Enqueue(flight);
        }
        private void RemoveFlightsFromWaitingList(Flight flight)
        {
            var flights = GetRelevantFlightQueue(flight.FlightDirection);
            flights.Dequeue();
        }
        private void SendPlaneToRelevantStation(Flight flight, bool fromWaitingList)
        {
            var stations = GetRelevantStations(flight.FlightDirection);
            var FreeStation = stations.FirstOrDefault(st => !st.IsOccupied);
            if (FreeStation != null && FreeStation.PlaneArrived(new Plane(flight)))
            {
                FlightMoved?.Invoke(this,new FlightEvent(flight,null,FreeStation.StationDto));
                if(fromWaitingList)
                    RemoveFlightsFromWaitingList(flight);
            }
            else if (!fromWaitingList)
            {
                AddFlightsToWaitingList(flight);
            }
        }
        private void RegisterToStationEvents()
        {
            IEnumerable<IStation> empty = Enumerable.Empty<IStation>();
            var AllStations = (LandStations ?? empty).Concat(TakeoffStations ?? empty);

            foreach (var station in AllStations)
            {
                station.FlightMoved += Station_Available;
            }
        }
        private void UnregisterToStationEvents()
        {
            IEnumerable<IStation> empty = Enumerable.Empty<IStation>();
            var AllStations = (LandStations ?? empty).Concat(TakeoffStations ?? empty);

            foreach (var station in AllStations)
            {
                station.FlightMoved -= Station_Available;
            }
        }
        private void Station_Available(object sender, EventArgs e)
        {
            if (sender is IStation station)
            {
                var landstation = LandStations.Contains(station);
                var takeoffstation = TakeoffStations.Contains(station);
                lock (LockObj)
                {
                    if (landstation && WaitingLandingFlights.TryPeek(out Flight Landingflight))
                    {
                        SendPlaneToRelevantStation(Landingflight, true);
                    }
                    else if (takeoffstation && WaitingTakeoffFlights.TryPeek(out Flight Takeoffflight))
                    {
                        SendPlaneToRelevantStation(Takeoffflight, true);
                    }
                }
            }
            else
            {
                throw new Exception("sender should be a station");
            }
        }
        
    }
}
