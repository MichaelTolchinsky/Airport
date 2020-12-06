using Common.Events;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api
{
    public interface IAirportNotifierService
    {
        void NotifyFlightMoved(FlightEvent flightEvent);
        void NotifyNewFlight(Flight flight);
    }
}
