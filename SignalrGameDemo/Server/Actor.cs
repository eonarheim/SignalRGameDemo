using System;
using SignalrGameDemo.Server.LinearAlgebra;

namespace SignalrGameDemo.Server
{
    public class Actor
    {
        public Actor()
        {
            Dir = 3*(float)Math.PI/2;
            Pos = new Point(1,1);
            Vel = new Vector();
        }

        // in radians
        public float Dir { get; set; }
        public Point Pos { get; set; }
        public Vector Vel { get; set; }

        public virtual void Update(float delta, Game game)
        {
            Pos = Pos.Add(Vel);
        }
    }
}