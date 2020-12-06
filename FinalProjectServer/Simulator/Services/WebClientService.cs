using Common.Models;
using Simulator.Api;
using System.Net.Http;
using System.Threading.Tasks;

namespace Simulator.Services
{
    public class WebClientService : IWebClientService
    {
        private readonly HttpClient httpClient;

        public WebClientService()
        {
            httpClient = new HttpClient();
        }
  
        public async Task SendFlight(Flight flight)
        {
            await httpClient.PostAsJsonAsync("http://localhost:53109/api/Airport/RecievePlane", flight);
        }
    }
}
