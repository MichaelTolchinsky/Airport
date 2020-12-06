using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class FlightDto
    {
        public int Id { get; set; }
        public DirectionEnum FlightDirection { get; set; }
        public DateTime ScheduledTime { get; set; }

        public static FlightDto CreateFlightDto(Flight flight)
        {
            return new FlightDto{Id = flight.Id,FlightDirection=flight.FlightDirection,ScheduledTime = flight.ScheduledTime };
        }
    }
}
