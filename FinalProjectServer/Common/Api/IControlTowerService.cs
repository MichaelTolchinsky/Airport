using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Api
{
    public interface IControlTowerService : INextStation,IPlaneHandler
    {
        ControlTower ControlTower{ get;}
    }
}
