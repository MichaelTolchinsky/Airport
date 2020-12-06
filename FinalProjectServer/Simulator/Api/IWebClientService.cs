using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Api
{
    public interface IWebClientService
    {
        public Task SendFlight(Flight flight);
    }
}
