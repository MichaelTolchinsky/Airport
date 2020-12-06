using Common.Api;

namespace Common.Models
{
    public class StationRelation : IRelatedToNextStation
    {
        public int StationFromId{ get; set; }
        public Station StationFrom { get; set; }
        public int StationToId { get; set; }
        public Station StationTo { get; set; }
        public DirectionEnum Direction{ get; set; }
    }
}
