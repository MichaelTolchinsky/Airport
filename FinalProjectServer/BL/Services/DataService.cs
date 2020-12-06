using Common.Api;
using Common.Dtos;
using Common.Models;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Services
{
    public class DataService : IDataService
    {
        private readonly IRepository<Flight> FlightRepository;
        private readonly IRepository<Station> StationRepository;
        private readonly IRepository<ControlTower> ControlTowerRepository;
        private readonly IAirportService airportService;
        private readonly IAirportNotifierService airportNotifierService;

        public DataService(
            IRepository<Flight> FlightRepository,
            IRepository<Station> StationRepository,
            IRepository<ControlTower> ControlTowerRepository,
            IAirportService airportService,
            IAirportNotifierService airportNotifierService
            )
        {
            this.FlightRepository = FlightRepository;
            this.StationRepository = StationRepository;
            this.ControlTowerRepository = ControlTowerRepository;
            this.airportService = airportService;
            this.airportNotifierService = airportNotifierService;
            BuildRelations();
            LoadWaitingFlightsFromDb();
        }

        public DataService()
        {
        }

        public ControlTower GetControlTower() => ControlTowerRepository.GetAll().FirstOrDefault();
        public async Task HandleFlightArrived(Flight flight)
        {
            flight.ControlTowerId = airportService.ControlTowerService.ControlTower.Id;
            Flight savedFlight = await FlightRepository.AddAsync(flight);
            airportNotifierService.NotifyNewFlight(savedFlight);
            var timeToWait = flight.ScheduledTime - DateTime.Now;
            if (timeToWait > TimeSpan.Zero)
                await Task.Delay(timeToWait);
            airportService.PlaneArrived(flight);
        }
        public DataDto GetData()
        {
            var LandingFlights = GetLandingFlights();
            var TakeoffFlights = GetTakeoffFlights();
            var Stations = GetStations();
            return new DataDto { LandingFlights = LandingFlights, TakeoffFlights = TakeoffFlights, Stations = Stations };
        }
        private void LoadWaitingFlightsFromDb()
        {
            if (!airportService.IsInit)
            {
                var LoadedFlights = FlightRepository.GetAll().Where(f => f.FlightHistory.Count <= 0);
                foreach (Flight item in LoadedFlights)
                {
                    airportService.PlaneArrived(item);
                }
            }
            airportService.FlightsLoaded();
        }
        private void BuildRelations()
        {
            var controlTower = ControlTowerRepository.GetAll().FirstOrDefault();
            var stations = StationRepository.GetAll().ToList();
            airportService.BuildRelation(controlTower, stations);
        }
        private IEnumerable<FlightDto> GetLandingFlights()
        {
            return FlightRepository.GetAll().Where(f => f.FlightDirection == DirectionEnum.Landing && f.FlightHistory.Count == 0).Select(f => FlightDto.CreateFlightDto(f));
        }
        private IEnumerable<FlightDto> GetTakeoffFlights()
        {
            return FlightRepository.GetAll().Where(f => f.FlightDirection == DirectionEnum.Takeoff && f.FlightHistory.Count == 0).Select(f => FlightDto.CreateFlightDto(f));
        }
        private IEnumerable<StationDto> GetStations()
        {
            return StationRepository.GetAll().AsEnumerable().Select(s => StationDto.CreateStationDto(s));
        }
       
    }
}
