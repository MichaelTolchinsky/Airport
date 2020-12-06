using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Api
{
    public interface IFlightGeneratorService
    {
        public Task StartGeneratingRandomFlights();
    }
}
