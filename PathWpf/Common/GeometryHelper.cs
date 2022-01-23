using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AnimationPathWpf
{
    public static class GeometryHelper
    {
        public static double GetLength(this Geometry geo)
        {
            PathGeometry path = geo.GetFlattenedPathGeometry();

            double length = 0.0;

            foreach (PathFigure pf in path.Figures)
            {
                Point start = pf.StartPoint;

                foreach (PathSegment seg in pf.Segments)
                {
                    if (seg is PolyLineSegment pls)
                        foreach (Point point in pls.Points)
                        {
                            length += Distance(start, point);
                            start = point;
                        }
                    else if (seg is LineSegment ls)
                    {
                        var point = ls.Point;
                        length += Distance(start, point);
                        start = point;
                    }
                }
            }

            return length;
        }

        public static Point GetEndPoint(this Geometry geo)
        {
            PathGeometry path = geo.GetFlattenedPathGeometry();

            var seg = path.Figures.Last().Segments.Last();
            switch (seg)
            {
                case PolyLineSegment pls:
                    return pls.Points.Last();

                case LineSegment ls:
                    return ls.Point;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }
    }
}