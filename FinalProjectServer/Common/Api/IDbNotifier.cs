using Common.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api
{
    public interface IDbNotifier
    {
        public void NotifyFlightMoved(FlightEvent flightEvent);
    }
}
