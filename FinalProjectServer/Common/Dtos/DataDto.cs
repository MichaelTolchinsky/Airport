using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class DataDto
    {
        public IEnumerable<FlightDto> LandingFlights { get; set; }
        public IEnumerable<FlightDto> TakeoffFlights { get; set; }
        public IEnumerable<StationDto> Stations{ get; set; }
    }
}
