using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api
{
    public interface IStation : IPlaneHandler,INextStation
    {
        public Station StationDto { get; set; }
        public IPlane Plane { get; set; }
        public bool IsOccupied { get; }
   }
}
