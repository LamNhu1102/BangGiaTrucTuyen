using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(BangGiaTrucTuyen.Startup))]

namespace BangGiaTrucTuyen
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            //ConfigureAuth(app);
            app.MapSignalR();
        }

    }
}
