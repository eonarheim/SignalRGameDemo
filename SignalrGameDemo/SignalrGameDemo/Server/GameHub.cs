using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalrGameDemo.Server
{
    public class GameHub : Hub
    {
        private readonly Game _game;
        public GameHub() : this(Game.Instance) { }
        public GameHub(Game game)
        {
            _game = game;
        }
        
        public void Hello()
        {
            Clients.All.hello("Hello from server!");
        }
        
        public void Move(string direction)
        {
            var connectionId = Context.ConnectionId;
            var player = _game.Players[connectionId];
            if (direction == Commands.Forward.ToString())
            {
                player.moveForward();
            }

            if (direction == Commands.Backward.ToString())
            {
                player.moveBackward();
            }

        }

        public void Rotate(string direction)
        {
            var connectionId = Context.ConnectionId;
            var player = _game.Players[connectionId];
            if (direction == Commands.TurnLeft.ToString())
            {
                player.turnLeft();
            }

            if (direction == Commands.TurnRight.ToString())
            {
                player.turnRight();
            }

        }

        public void Fire()
        {
            var connectionId = Context.ConnectionId;
            var player = _game.Players[connectionId];
            _game.AddBullet(connectionId, player);
            
        }

        public override Task OnDisconnected()
        {
            var connectionId = Context.ConnectionId;
            _game.Players.Remove(connectionId);
            _game.Bullets.Remove(connectionId);
            return base.OnDisconnected();
        }

        public override Task OnConnected()
        {
            var connectionId = Context.ConnectionId;

            var player = new Player(connectionId);
            _game.AddPlayer(connectionId, new Player(connectionId));
            Clients.Caller.connect(player);

            _game.Bullets.Add(connectionId, new List<Bullet>());
            return base.OnConnected();
        }

        
    }
}