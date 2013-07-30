using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalrGameDemo.Server
{
    [HubName("Chat")]
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("Welcome to Chat");
        }

        public void Send(string nickname, string message)
        {
            Clients.Others.handleMessage(nickname, message);
        }

        public override Task OnConnected()
        {
            var id = Context.ConnectionId;
            Clients.Caller.handleMessage("[debug]", "Your connection id: " + id);
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            var id = Context.ConnectionId;
            Clients.Others.handleMessage("[debug]", "ClientId: " + id + " has disconnected");
            return base.OnDisconnected();
        }

        public void Test()
        {
            
	        var id = Context.ConnectionId;// is your friend

            Clients.All.clientFunctionName();// - Everyone to the server
            Clients.Others.clientFunctionName();// - Everyone but the Caller to the server
            Clients.Caller.clientFunctionName();// - Just the Caller to the server


        }
    }
}