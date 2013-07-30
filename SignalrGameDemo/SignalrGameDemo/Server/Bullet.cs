using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrGameDemo.Server
{
    public class Bullet : Actor
    {
        private static int _id;
        public Bullet()
        {
            Life = 50;
            Id = _id++;
            Speed = 20;
        }

        public Bullet(Player player)
            : this()
        {
            Dx = (float)Math.Cos(player.Dir) * Speed;
            Dy = (float)Math.Sin(player.Dir) * Speed;



            X = player.X  + 50* (float) (Math.Cos(player.Dir) * Math.Cos(-Math.PI/4));
            Y = player.Y + 50* (float) (Math.Sin(player.Dir) * Math.Sin(-Math.PI/4)) ;
            Dir = player.Dir;
        }

        public float Speed { get; set; }
        public int Life { get; set; }
        public int Id { get; set; }

        public override void Update(float delta, Game game)
        {
            base.Update(delta, game);
            Life--;

        }
    }
}