using Common.Api;
using Common.Models;
using System;
using System.Threading.Tasks;

namespace XUnitTest
{
    public class MockPlane : IPlane
    {
        public Flight Flight { get ; set ; }
        public event EventHandler ReadyToContinue;

        public MockPlane()
        {
            Flight = new Flight();
        }

        public async void StartWaitingInStation()
        {
            await Task.Delay(2000);
        }

        public void StopWaiting()
        {
            ReadyToContinue?.Invoke(this,null);
        }
    }
}
