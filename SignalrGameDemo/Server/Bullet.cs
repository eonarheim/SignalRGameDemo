using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalrGameDemo.Server.LinearAlgebra;

namespace SignalrGameDemo.Server
{
    public class Bullet : Actor
    {
        private static int _id;
        public Bullet()
        {
            Life = 100;
            Id = _id++;
            Speed = 20;
        }

        public Bullet(Player player)
            : this()
        {
            Vel = Vel.Rotate(player.Dir).Scale(Speed);
            this.Pos.X = player.Pos.X + 100;
            this.Pos.Y = player.Pos.Y + 50;
            var origin = new Point(player.Pos.X + 50, player.Pos.Y + 50);

            this.Pos = this.Pos.RotateAbout(origin, player.Dir);

            Dir = player.Dir;
        }

        public float Speed { get; set; }
        public int Life { get; set; }
        public int Id { get; set; }

        public override void Update(float delta, Game game)
        {
            foreach (var p in game.Players.Values)
            {
                var centerPlayer = p.Pos.Add(new Vector(50, 50));
                if (this.Pos.Distance(centerPlayer) < 45)
                {
                    this.Life = -1;
                    p.Life -= 10;
                }

            }
            base.Update(delta, game);
            Life--;

        }
    }
}
