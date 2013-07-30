using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalrGameDemo.Server.LinearAlgebra;

namespace SignalrGameDemo.Server
{
    public class Player : Actor
    {
        private static int _id;
        private float speed = 5; //px/s
        public Player(string connectionId)
        {
            Life = 100;
            this.ConnectionId = connectionId;
            var rand = new Random((int)DateTime.Now.Ticks + DateTime.UtcNow.Millisecond );
            Pos = new Point(rand.Next(0, 400), rand.Next(0, 400));
        }

        public string ConnectionId { get; private set; }
        public string Name { get; set; }

        public void moveForward()
        {
            Vel = new Vector().Rotate(Dir).Scale(speed);
        }

        public void moveBackward()
        {
            Vel = new Vector().Rotate(Dir).Scale(-speed);
        }
        
        public void turnLeft()
        {
            Dir -= 5 * (float)(Math.PI / 180);
        }

        public void turnRight()
        {
            Dir += 5 * (float)(Math.PI / 180);
        }

        public override void Update(float delta, Game game)
        {
            base.Update(delta, game);
            Vel.X = 0;
            Vel.Y = 0;
        }

        public int Life { get; set; }
    }
}