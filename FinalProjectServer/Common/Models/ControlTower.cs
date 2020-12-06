using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace Common.Models
{
    public class ControlTower
    {
        private readonly ILazyLoader lazyLoader;
        private ICollection<ControlTowerStationRelation> startupStations;
        public ControlTower()
        {

        }
        public ControlTower(ILazyLoader lazyLoader)
        {
            this.lazyLoader = lazyLoader;
        }
        public int Id { get; set; }
        public ICollection<ControlTowerStationRelation> StartupStations { get => lazyLoader.Load(this,ref startupStations); set => startupStations = value; }
        public ICollection<Flight> Flights { get; set; }
    }
}
