using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrGameDemo.Server
{
    public class Player : Actor
    {
        private static int _id;
        private float speed = 5; //px/s
        public Player(string connectionId)
        {
            this.ConnectionId = connectionId;
        }

        public string ConnectionId { get; private set; }
        public string Name { get; set; }

        public void moveForward()
        {
            Dx = (float) Math.Cos(Dir)*speed;
            Dy = (float) Math.Sin(Dir)*speed;
        }

        public void moveBackward()
        {
            Dx = (float) Math.Cos(Dir) * -speed;
            Dy = (float) Math.Sin(Dir) * -speed;    
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
            Dx = 0;
            Dy = 0;

        }
        
    }
}