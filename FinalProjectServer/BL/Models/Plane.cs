using Common.Api;
using Common.Models;
using System;
using System.Threading.Tasks;

namespace BL.Models
{
    public class Plane : IPlane
    {
        public Flight Flight { get; set; }
        public event EventHandler ReadyToContinue;
        private readonly Random random;
        public Plane(Flight flight)
        {
            random = new Random();
            Flight = flight;
        }

        public async void StartWaitingInStation()
        {
            await Task.Delay(random.Next(3000, 12000));
            ReadyToContinue?.Invoke(this,null);
        }
    }
}
