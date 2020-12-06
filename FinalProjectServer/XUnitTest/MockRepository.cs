using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest
{
    class MockRepository
    {
        public Flight currentAddedFlight { get; set; }

        public void AddFlight(Flight flight)
        {
            currentAddedFlight = flight;
        }
    }
}
