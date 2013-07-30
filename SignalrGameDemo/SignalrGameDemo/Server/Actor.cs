using System;

namespace SignalrGameDemo.Server
{
    public class Actor
    {
        public Actor()
        {
            Dir = 3*(float)Math.PI/2;
            X = 0;
            Y = 0;
            Dx = 0;
            Dy = 0;
        }

        // in radians
        public float Dir { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Dx { get; set; }
        public float Dy { get; set; }

        public virtual void Update(float delta, Game game)
        {
            X += Dx;
            Y += Dy;
        }
    }
}