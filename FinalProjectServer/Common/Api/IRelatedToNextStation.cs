using Common.Models;

namespace Common.Api
{
    public interface IRelatedToNextStation
    {
        public int StationToId { get; set; }
        public DirectionEnum Direction{ get; set; }
    }
}