using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api
{
    public interface INextStation
    {
        public ICollection<IStation> LandStations { get;set; }
        public ICollection<IStation> TakeoffStations { get; set;}
        IEnumerable<IRelatedToNextStation> RelatedToStations { get; }
        void SetStations(ICollection<IStation> LandStations, ICollection<IStation> TakeoffStations);
    }
}
