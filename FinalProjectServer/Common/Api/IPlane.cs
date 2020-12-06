using Common.Models;
using System;

namespace Common.Api
{
    public interface IPlane
    {
        Flight Flight { get; set; }
        event EventHandler ReadyToContinue;
        void StartWaitingInStation();
    }
}
