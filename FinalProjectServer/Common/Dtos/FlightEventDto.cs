using Common.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class FlightEventDto
    {
        public FlightDto Flight { get; set; }
        public StationDto StationFrom { get; set; }
        public StationDto StationTo { get; set; }
        public static FlightEventDto CreateFlightEventDto(FlightEvent flightEvent)
        {
            StationDto stationDtoFrom = flightEvent.StationFrom == null ? null : StationDto.CreateStationDto(flightEvent.StationFrom);
            StationDto stationDtoTo = flightEvent.StationTo == null ? null : StationDto.CreateStationDto(flightEvent.StationTo);
            return new FlightEventDto
            {
                Flight = FlightDto.CreateFlightDto(flightEvent.Flight),
                StationFrom = stationDtoFrom,
                StationTo = stationDtoTo
            };
        }
    }
}
