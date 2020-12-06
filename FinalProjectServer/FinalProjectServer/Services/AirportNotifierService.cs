using Common.Api;
using Common.Dtos;
using Common.Events;
using Common.Models;
using FinalProjectServer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FinalProjectServer.Services
{
    public class AirportNotifierService : IAirportNotifierService
    {
        private readonly IHubContext<AirportHub> hubContext;

        public AirportNotifierService(IHubContext<AirportHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public void NotifyFlightMoved(FlightEvent flightEvent)
        {
            hubContext.Clients.All.SendAsync("FlightMoved",FlightEventDto.CreateFlightEventDto(flightEvent));
        }

        public void NotifyNewFlight(Flight flight)
        {
            hubContext.Clients.All.SendAsync("NewFlight",FlightDto.CreateFlightDto(flight));
        }
    }
}
