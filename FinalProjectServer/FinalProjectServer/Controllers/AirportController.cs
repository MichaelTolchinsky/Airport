using Common.Api;
using Common.Dtos;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinalProjectServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IDataService dataService;

        public AirportController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet]
        public void StartSystem()
        {

        }

        [HttpGet]
        [Route("GetData")]
        public ActionResult<DataDto> GetData()
        {
            try
            { 
                return dataService.GetData();
            }
            catch (Exception e)
            {
                return StatusCode(500,e.Message);
            }

        }

        [HttpPost]
        [Route("RecievePlane")]
        public void RecievePlane(Flight flight)
        {
            if (flight is null)
            {
                throw new ArgumentNullException($"{nameof(flight)} can't be null");
            }
            else
            {
                dataService.HandleFlightArrived(flight);
            }
        }




    }
}
