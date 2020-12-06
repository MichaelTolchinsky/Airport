using Common.Models;
using System;

namespace Common.Events
{
    public class FlightEvent : EventArgs
    {

        public Flight Flight { get; }
        public Station StationFrom { get; }
        public Station StationTo { get; }

        public FlightEvent(Flight flight, Station stationFrom, Station stationTo)
        {
            Flight = flight;
            StationFrom = stationFrom;
            StationTo = stationTo;
        }
        public bool IsFromControlTowerToFirstStation => StationFrom is null && StationTo != null;
        public bool IsFromLastStationToEnd => StationFrom != null && StationTo is null;
    }
}
