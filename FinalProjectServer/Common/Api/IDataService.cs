using Common.Dtos;
using Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Api
{
    public interface IDataService
    {
        Task HandleFlightArrived(Flight flight);
        ControlTower GetControlTower();
        DataDto GetData();
    }
}
