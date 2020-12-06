using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class StationDto
    {
        public int Id { get; set; }
        public int? CurrentFlightId { get; set; }
        public static StationDto CreateStationDto(Station staion)
        {
            return new StationDto { Id = staion.Id, CurrentFlightId = staion.CurrentFlightId };
        }
    }
}
