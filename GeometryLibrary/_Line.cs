using System;
using System.Windows;

namespace GeometryLibrary.WinForms
{
    // Winforms version of line
    public class Line
    {
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;

            A = End.Y - Start.Y;
            B = Start.X - End.X;
            C = A * Start.X + B * Start.Y;
            MidPoint = new Point((int)(Start.X + B / 2), (int)(Start.Y + A / 2));
        }

        public Point Start { get; private set; }
        public Point End { get; private set; }

        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public Point MidPoint { get; private set; }

        public Point? GetIntersectionWithLine(Line otherLine)
        {
            double determinant = A * otherLine.B - otherLine.A * B;

            if (determinant == 0) //lines are parrallel
                return default(Point?);

            //Cramer's Rule

            int x = (int)Math.Round((otherLine.B * C - B * otherLine.C) / determinant);
            int y = (int)((A * otherLine.C - otherLine.A * C) / determinant);

            Point intersectionPoint = new Point(x, y);

            return intersectionPoint;
        }

        public override string ToString()
        {
            return "[" + Start + "], [" + End + "]";
        }
    }
}