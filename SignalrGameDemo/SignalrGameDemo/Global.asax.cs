using System;
using System.Web;
using System.Web.Routing;

namespace SignalrGameDemo.Application_Start
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Register the default hubs route: ~/signalr
            RouteTable.Routes.MapHubs();
        }
    }
}