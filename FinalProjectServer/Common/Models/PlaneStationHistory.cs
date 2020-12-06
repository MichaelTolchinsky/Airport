using System;

namespace Common.Models
{
    public class PlaneStationHistory
    {
        public Guid Id { get; set; }
        public Flight Flight { get; set; }
        public int StationId { get; set; }
        public Station Station{ get; set; }
        public DateTime? EnterStationTime { get; set; }
        public DateTime? ExitStationTime { get; set; }
    }
}
