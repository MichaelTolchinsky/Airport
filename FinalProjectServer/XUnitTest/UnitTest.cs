using BL.Models;
using BL.Services;
using Common.Api;
using Common.Events;
using Common.Models;
using System.Collections.Generic;
using Xunit;
using Plane = BL.Models.Plane;
using StationLogic = BL.Models.StationLogic;

namespace XUnitTest
{
    public class UnitTest
    {
        [Fact]
        public void FlightDoNotGetAcceptedIfNoFreestation()
        {
            StationLogic station = new StationLogic(new Station());
            var plane = new MockPlane();

            Assert.True(station.PlaneArrived(plane));
            Assert.False(station.PlaneArrived(plane));
        }

        [Fact]
        public void FlightIsAcceptedIfStationIsFree()
        {
            IStation station1 = new StationLogic(new Station());
            IStation station2 = new StationLogic(new Station());
            station1.LandStations = new IStation[] { station2 };
            var plane = new MockPlane { Flight = new Flight() { FlightDirection = DirectionEnum.Landing } };
            Assert.True(station1.PlaneArrived(plane));

            plane.StopWaiting();

            Assert.False(station1.IsOccupied);
            Assert.Equal(station2.Plane, plane);
        }

        [Fact]
        public void FlightStayInStationIfNoFreestation()
        {
            IStation station1 = new StationLogic(new Station());
            IStation station2 = new StationLogic(new Station());
            station1.TakeoffStations = new IStation[] { station2 };
            var plane1 = new MockPlane { Flight = new Flight{FlightDirection = DirectionEnum.Takeoff } };
            var plane2 = new MockPlane { Flight = new Flight { FlightDirection = DirectionEnum.Takeoff } };

            Assert.True(station1.PlaneArrived(plane1));
            Assert.True(station2.PlaneArrived(plane2));

            plane1.StopWaiting();

            Assert.True(station1.IsOccupied);
            Assert.NotEqual(station2.Plane, plane1);
        }

        [Fact]
        public void StationIsEmptyAfterPlaneMovedToNextStation()
        {
            IStation station1 = new StationLogic(new Station());
            IStation station2 = new StationLogic(new Station());
            station1.LandStations = new IStation[] { station2 };

            var plane1 = new MockPlane{Flight=new Flight{FlightDirection = DirectionEnum.Landing } };
            station1.PlaneArrived(plane1);
            plane1.StopWaiting();

            Assert.True(!station1.IsOccupied);
            Assert.Equal(station2.Plane, plane1);
        }

        [Fact]
        public void LastStationIsFreeAfterPlaneFinishedWaiting()
        {
            IStation station1 = new StationLogic(new Station());
            station1.LandStations = new IStation[]{ } ;
            var plane1 = new MockPlane { Flight = new Flight{FlightDirection=DirectionEnum.Landing } };

            Assert.True(station1.PlaneArrived(plane1));

            plane1.StopWaiting();

            Assert.False(station1.IsOccupied);
        }

        [Fact]
        public void PlaneTransfersToNextStationOnlyIfItsFree()
        {
            IStation station1 = new StationLogic(new Station());
            IStation station2 = new StationLogic(new Station());
            station1.LandStations = new IStation[] { station2 };
            var plane = new MockPlane{Flight = new Flight{FlightDirection= DirectionEnum.Landing } };
            
            Assert.True(station1.PlaneArrived(plane));
            plane.StopWaiting();
            Assert.False(station1.IsOccupied);
            Assert.Equal(station2.Plane, plane);
        }

        [Fact]
        public void PlanesGetToTheLandingOrTakeoffStation()
        {
            IStation station = new StationLogic(new Station());
            IStation stationLanding = new StationLogic(new Station());
            IStation stationTakeoff = new StationLogic(new Station());
            station.LandStations = new IStation[]{stationLanding };
            station.TakeoffStations = new IStation[] { stationTakeoff };

            var plane1 = new MockPlane {Flight = new Flight{FlightDirection = DirectionEnum.Landing } };
            var plane2 = new MockPlane { Flight = new Flight { FlightDirection = DirectionEnum.Takeoff } };

            Assert.True(station.PlaneArrived(plane1));
            Assert.False(!station.IsOccupied);
            Assert.Equal(station.Plane, plane1);

            Assert.True(!stationLanding.IsOccupied);
            Assert.True(!stationTakeoff.IsOccupied);

            plane1.StopWaiting();
            Assert.True(!station.IsOccupied);
            Assert.False(!stationLanding.IsOccupied);
            Assert.Equal(stationLanding.Plane, plane1);

            Assert.True(station.PlaneArrived(plane2));
            Assert.False(!station.IsOccupied);
            Assert.Equal(station.Plane, plane2);

            plane2.StopWaiting();

            Assert.True(!station.IsOccupied);
            Assert.False(!stationTakeoff.IsOccupied);
            Assert.Equal(stationTakeoff.Plane, plane2);
        }

        [Fact]
        public void StationDirectionAvailbalePlane()
        {
            IStation station = new StationLogic(new Station());
            IStation landingStation = new StationLogic(new Station());
            IStation takeoffStation = new StationLogic(new Station());
            station.LandStations = new IStation[] { landingStation };
            station.TakeoffStations = new IStation[] { takeoffStation };
            var takeoffPlane = new MockPlane { Flight = new Flight{FlightDirection=DirectionEnum.Takeoff } };
            var waitingPlane = new MockPlane();

            Assert.True(takeoffStation.PlaneArrived(waitingPlane));
            Assert.True(station.PlaneArrived(takeoffPlane));
            takeoffPlane.StopWaiting();

            Assert.Equal(takeoffStation.Plane, waitingPlane);
            Assert.False(landingStation.IsOccupied);
            Assert.True(station.IsOccupied);
        }

        [Fact]
        public void AddedFlightGoToUiandRepository()
        {

            IStation enteraceStation = new StationLogic(new Station());
            MockNotifier notifierService = new MockNotifier();
            MockRepository mockRepository = new MockRepository ();
            var plane = new MockPlane{Flight = new Flight{FlightDirection = DirectionEnum.Landing } };
            notifierService.NotifyFlightMoved(new FlightEvent(plane.Flight,null,enteraceStation.StationDto));
            mockRepository.AddFlight(plane.Flight);

            enteraceStation.PlaneArrived(plane);
            Assert.True(enteraceStation.IsOccupied);
            Assert.Equal(notifierService.FlightEventNotification.Flight,plane.Flight);
            Assert.Equal(mockRepository.currentAddedFlight, plane.Flight);
        }

    }
}