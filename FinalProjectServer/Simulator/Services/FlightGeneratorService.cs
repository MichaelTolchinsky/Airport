using Common.Models;
using Simulator.Api;
using System;
using System.Threading.Tasks;

namespace Simulator.Services
{
    public class FlightGeneratorService : IFlightGeneratorService
    {
        private readonly IRandomDataService randomData;
        private readonly IWebClientService webClientService;

        public FlightGeneratorService(IRandomDataService randomData, IWebClientService webClientService)
        {
            this.randomData = randomData;
            this.webClientService = webClientService;
        }
        public async Task StartGeneratingRandomFlights()
        {
            for (; ; )
            {
                await randomData.RandomDelay(5,10);
                await SendFlightToServer();
            }
        }
        private async Task<Flight> SendFlightToServer()
        {
            Flight flight = CreateFlight();
            await webClientService.SendFlight(flight);
            return flight;
        }
        private Flight CreateFlight()
        {
            DirectionEnum direction = randomData.RandomFlightDirection();
            int RandomDelay = randomData.RandomNumber(15, 35);
            DateTime ScheduledTime = DateTime.Now.AddSeconds(RandomDelay);
            return new Flight { FlightDirection = direction, ScheduledTime = ScheduledTime };
        }
    }
}
