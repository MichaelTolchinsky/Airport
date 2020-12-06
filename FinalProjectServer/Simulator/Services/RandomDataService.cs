using Common.Models;
using Simulator.Api;
using System;
using System.Threading.Tasks;

namespace Simulator.Services
{
    public class RandomDataService : IRandomDataService
    {
        private readonly Random random = new Random();
        public int RandomNumber(int min, int max)
        {
            if (max < min) throw new ArgumentOutOfRangeException(nameof(max), "Max number cant be smaller than the min");
            return random.Next(min, max);
        }
        public async Task RandomDelay(int minSeconds = 1, int maxSeconds = 5)
        {
            if (minSeconds < 0) throw new ArgumentOutOfRangeException(nameof(minSeconds), "Cannot delay negative time");
            int time = RandomNumber(minSeconds * 1000, maxSeconds * 1000);
            await Task.Delay(time);
        }
        public DirectionEnum RandomFlightDirection() => random.Next(1,3) % 2 == 0 ? DirectionEnum.Landing : DirectionEnum.Takeoff;
    }
}
