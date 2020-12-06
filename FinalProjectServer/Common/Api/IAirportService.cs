using Common.Models;
using System.Collections.Generic;

namespace Common.Api
{
    public interface IAirportService
   {
        ICollection<IStation> Stations { get; }
        IControlTowerService ControlTowerService { get;}
        public bool IsInit { get; }
        void BuildRelation(ControlTower controlTower,ICollection<Station> stations);
        void FlightsLoaded();
        void PlaneArrived(Flight flight);
   }
}
