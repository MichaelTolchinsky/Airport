using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace Common.Models
{
    public class Station
    {
        private readonly ILazyLoader lazyLoader;
        private ICollection<StationRelation> childrenStations;
        public Station()
        {

        }
        public Station(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        public int Id { get; set; }
        public int? CurrentFlightId { get; set; }
        public Flight CurrentFlight { get; set; }
        public ControlTowerStationRelation ControlTowerStationRelation { get; set; }
        public ICollection<StationRelation> ParentStations { get; set; }
        public ICollection<StationRelation> ChildrenStations { get => lazyLoader.Load(this,ref childrenStations); set => childrenStations = value; }
        public ICollection<PlaneStationHistory> StationHistory { get; set; }
    }
}
