using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalrGameDemo.Server.LinearAlgebra
{
    public class Vector
    {
        public Vector()
        {
            X = 1;
            Y = 0;
        }

        public Vector(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public Vector Rotate(double radians)
        {
            var cosTheta = Math.Cos(radians);
            var sinTheta = Math.Sin(radians);

            var newX = cosTheta*this.X - sinTheta*this.Y;
            var newY = sinTheta*this.X + cosTheta*this.Y;
            return new Vector(newX, newY);

        }
        public Vector Add(Vector vector)
        {
            return new Vector(this.X + vector.X, this.Y + vector.Y);
        }
        public Point Add(Point point)
        {
            return new Point(point.X + this.X, point.Y + this.Y);
        }

        public Vector Normalize()
        {
            var magnitude = Math.Sqrt(this.X*this.X + this.Y * this.Y);
            if (magnitude > 0)
            {
                return new Vector(this.X/magnitude, this.Y/magnitude);
            }
            else
            {
                Console.WriteLine("DOES THIS EVEN HAPPEN!!!");
                return this;
            }
        }
        public Vector Scale(double magnitude)
        {
            var tmpVector = this.Normalize();
            return new Vector(tmpVector.X * magnitude, tmpVector.Y * magnitude);
        }


    }
}