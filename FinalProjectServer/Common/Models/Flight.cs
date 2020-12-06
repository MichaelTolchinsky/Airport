using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class Flight
    {
        private readonly ILazyLoader lazyLoader;
        private ICollection<PlaneStationHistory> flightHistory;
        public Flight()
        {

        }
        public Flight(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        public int Id { get; set; }
        public DirectionEnum FlightDirection { get; set; }
        public int PlaneId { get; set; }
        public DateTime ScheduledTime { get; set; }
        public int? StationId { get; set; }
        public Station Station { get; set; }
        public int ControlTowerId { get; set; }
        public ControlTower ControlTower { get; set; }
        public ICollection<PlaneStationHistory> FlightHistory { get => lazyLoader.Load(this,ref flightHistory); set => flightHistory = value; }
    }
}
