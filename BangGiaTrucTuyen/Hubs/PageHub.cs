using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BangGiaTrucTuyen.Hubs
{
    public class PageHub : Hub
    {
        public static void Show()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<PageHub>();
            context.Clients.All.displayPage();
        }
    }
}