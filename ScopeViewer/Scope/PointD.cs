using System;
using System.Drawing;

namespace ScopeViewer.Scope
{
    public struct PointD
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }


        public double Distance(PointD p2)
        {
            return Math.Sqrt(Math.Pow(X - p2.X, 2) + Math.Pow(Y - p2.Y, 2));
        }


        public void Scale(double offsetX, double scaleX, double offsetY, double scaleY)
        {
            X = offsetX + X * scaleX;
            Y = offsetY + Y * scaleY;
        }

        public static implicit operator PointF(PointD d) => new PointF((float)d.X, (float)d.Y);
        public static implicit operator PointD(Point d) => new PointD((float)d.X, (float)d.Y);
    }

}
