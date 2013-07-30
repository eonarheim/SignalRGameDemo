using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalrGameDemo.Server
{
    public class TestConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Send(connectionId, "Welcome!");
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            // Dynamic Dispatch Yourself!!!
           if (data.StartsWith("mymessage:"))
           {
               return Connection.Send(connectionId, "You called my message");
           }
           return Connection.Broadcast(data);
        }
    }
}