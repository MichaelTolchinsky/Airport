using Common.Events;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Common.Api
{
    public interface IPlaneHandler
    {
        event EventHandler<FlightEvent> FlightMoved;
        bool PlaneArrived(IPlane plane);
    }
}
