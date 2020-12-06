using Common.Api;
using Common.Events;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest
{
    class MockNotifier : IAirportNotifierService
    {
        public FlightEvent FlightEventNotification { get; private set; }

        public void NotifyFlightMoved(FlightEvent flightEvent)
        {
            FlightEventNotification = flightEvent;
        }

        public void NotifyNewFlight(Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
