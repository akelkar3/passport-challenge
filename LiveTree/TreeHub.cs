using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LiveTree.Controllers;
using Microsoft.AspNet.SignalR;

namespace LiveTree
{
    public class TreeHub : Hub
    {
        public void SendUpdate()
        {
            FactoriesController ctrl = new FactoriesController();

            Clients.All.updareFactories(ctrl.GetFactories());
        }
    }
}