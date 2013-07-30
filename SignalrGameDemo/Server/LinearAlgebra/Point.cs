using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrGameDemo.Server.LinearAlgebra
{
    public class Point
    {
        public Point()
        {
            X = 0;
            Y = 0;
        }

        public Point(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public Vector Minus(Point point)
        {
            return new Vector(this.X - point.X, this.Y - point.Y);
        }

        public Point Add(Vector vector)
        {
            return new Point(this.X + vector.X, this.Y + vector.Y);
        }

        public Point RotateAbout(Point origin, double radians)
        {
            var cosTheta = Math.Cos(radians);
            var sinTheta = Math.Sin(radians);
            var newX = cosTheta*(this.X - origin.X) - sinTheta*(this.Y - origin.Y) + origin.X;
            var newY = sinTheta*(this.X - origin.X) + cosTheta*(this.Y - origin.Y) + origin.Y;
            return new Point(newX, newY);
        }

        public double Distance(Point point)
        {
            return Math.Sqrt(Math.Pow(this.X - point.X, 2.0) + Math.Pow(this.Y - point.Y, 2.0));   
        }

    }
}