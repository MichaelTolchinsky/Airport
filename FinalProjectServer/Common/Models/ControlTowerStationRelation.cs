using Common.Api;

namespace Common.Models
{
    public class ControlTowerStationRelation : IRelatedToNextStation
    {
        public int StationToId { get; set; }
        public Station StationTo { get; set; }
        public int ControlTowerId { get; set; }
        public ControlTower ControlTower { get; set; }
        public DirectionEnum Direction { get; set; }
    }
}
