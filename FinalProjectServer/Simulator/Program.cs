using Microsoft.Extensions.DependencyInjection;
using Simulator.Api;
using Simulator.Services;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Simulator
{
    class Program
    {
        static async Task Main(string[] args)
        {


            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRandomDataService, RandomDataService>()
                .AddSingleton<IFlightGeneratorService, FlightGeneratorService>()
                .AddSingleton<IWebClientService, WebClientService>()
                .BuildServiceProvider();


            IFlightGeneratorService flightGenerator = serviceProvider.GetService<IFlightGeneratorService>();

            await SendBuildSystemRequest();
            await flightGenerator.StartGeneratingRandomFlights();

        }

        //private async static void SendNewPlane()
        //{
        //    var Flight = FlightGenerator.GenerateNewPlane();
        //    var httpClient = new HttpClient();
        //    await Task.Delay(random.Next(1, 10) * 1000);
        //    await httpClient.PostAsJsonAsync("http://localhost:53109/api/Airport/RecievePlane", Flight);
        //}

        private async static Task SendBuildSystemRequest()
        {
            var httpClient = new HttpClient();
            await httpClient.GetAsync("http://localhost:53109/api/Airport");
        }
    }
}
