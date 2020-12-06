using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Api
{
    public interface IRandomDataService
    {
        public int RandomNumber(int min = 0, int max = 200);
        public Task RandomDelay(int minSeconds = 1, int maxSeconds = 5);
        public DirectionEnum RandomFlightDirection();
    }
}
