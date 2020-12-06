using Common.Api;
using Common.Events;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Models
{
    public class StationLogic : IStation
    {
        public IPlane Plane { get; set; }
        public bool IsOccupied => Plane != null;
        public ICollection<IStation> TakeoffStations { get; set; }
        public ICollection<IStation> LandStations { get; set; }
        public Station StationDto { get; set; }

        public IEnumerable<IRelatedToNextStation> RelatedToStations => StationDto.ChildrenStations ?? Enumerable.Empty<IRelatedToNextStation>();

        public event EventHandler<FlightEvent> FlightMoved;

        public StationLogic(Station StationDto)
        {
            this.StationDto = StationDto ?? throw new ArgumentNullException($"{nameof(StationDto)} can't be null");
        }

        public bool PlaneArrived(IPlane plane)
        {
            if (Plane != null)
                return false;
            else
            {
                Plane = plane;
                Plane.StartWaitingInStation();
                Plane.ReadyToContinue += Plane_ReadyToContinue;
                return true;
            }

        }
        public void SetStations(ICollection<IStation> LandStations, ICollection<IStation> TakeoffStations)
        {
            this.TakeoffStations = TakeoffStations;
            this.LandStations = LandStations;
        }
        private void Plane_ReadyToContinue(object sender, EventArgs e)
        {
            var NextRelavantStations = Plane.Flight.FlightDirection == DirectionEnum.Landing ? LandStations : TakeoffStations;
            if (NextRelavantStations.Count == 0) ChangeAvailabilty(null);
            else
            {
                var FreeStation = NextRelavantStations.FirstOrDefault(st => !st.IsOccupied);
                if (FreeStation != null)
                {
                    FreeStation.PlaneArrived(Plane);
                    ChangeAvailabilty(FreeStation);
                }
                else
                {
                    foreach (var station in NextRelavantStations)
                    {
                        station.FlightMoved += NextStation_Available;
                    }
                }
            }
        }
        private void NextStation_Available(object sender, EventArgs e)
        {
            if (Plane == null) return;
            if (!(sender is StationLogic availableStation))
            {
                throw new Exception("Sender is not a Station");
            }

            availableStation.PlaneArrived(Plane);
            var NextRelavantStations = Plane.Flight.FlightDirection == DirectionEnum.Landing ? LandStations : TakeoffStations;
            foreach (var station in NextRelavantStations)
            {
                station.FlightMoved -= NextStation_Available;
            }
            ChangeAvailabilty(availableStation);

        }
        private void ChangeAvailabilty(IStation NextStation)
        {
            var flightRef = Plane.Flight;
            Plane.ReadyToContinue -= Plane_ReadyToContinue;
            Plane = null;
            FlightMoved?.Invoke(this, new FlightEvent(flightRef,StationDto, NextStation?.StationDto));
        }
        
    }
}
