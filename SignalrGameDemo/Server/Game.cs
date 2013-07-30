using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalrGameDemo.Server.GameUtils;

namespace SignalrGameDemo.Server
{
    public class Game
    {

        private readonly static Lazy<Game> _instance = new Lazy<Game>(() => new Game());

        private HighFrequencyTimer _mainLoop;
        private DateTime _lastDraw = DateTime.Now;
        private object _locker = new object();

        public ConcurrentDictionary<string, Player> Players { get; set; }
        public ConcurrentDictionary<string, List<Bullet>> Bullets { get; set; }


        public Game()
        {
            Players = new ConcurrentDictionary<string, Player>();
            Bullets = new ConcurrentDictionary<string, List<Bullet>>();

            _mainLoop = new HighFrequencyTimer(30.0, id => Update(id));
            _mainLoop.Start();
        }

        public void AddPlayer(string connectionId, Player player)
        {
            Players.TryAdd(connectionId, player);
        }

        public void AddBullet(string connectionId, Player player)
        {
            var bullet = new Bullet(player);
            if (Bullets[connectionId].Count < 10)
            {
                Bullets[connectionId].Add(bullet);
            }
        }



        public long Update(long id)
        {
            foreach (var player in Players.Values)
            {
                player.Update(20, this);
            }

            foreach (var bulletList in Bullets.Values)
            {
                bulletList.ForEach(b => b.Update(20, this));
                // Remove dead bullets
                bulletList.RemoveAll(b => b.Life < 0);
            }
            Draw();
            return id;
        }

        public void Draw()
        {
            var bullets = new List<Bullet>();
            foreach (var bulletList in Bullets.Values)
            {
                bullets.AddRange(bulletList);
            }
            var gameState = new { Players = Players.Values, Bullets = bullets };
            foreach (string connectionID in Players.Keys)
            {
                GetContext().Clients.Client(connectionID).updateGame(gameState);
            }
            _lastDraw = DateTime.Now;

        }

        public static IHubContext GetContext()
        {
            return GlobalHost.ConnectionManager.GetHubContext<GameHub>();
        }

        //obvious singleton joke here
        public static Game Instance
        {
            get { return _instance.Value; }

        }


    }
}
