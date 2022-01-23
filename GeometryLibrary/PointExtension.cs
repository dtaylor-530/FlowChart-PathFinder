using System;
using System.Collections.Generic;
using System.Windows;

namespace GeometryLibrary
{
    public static class PointExtension
    {
        public static double DistanceToPoint(this Point point, Point point2)
        {
            return Math.Sqrt((point2.X - point.X) * (point2.X - point.X) + (point2.Y - point.Y) * (point2.Y - point.Y));
        }

        public static double SquaredDistanceToPoint(this Point point, Point point2)
        {
            return (point2.X - point.X) * (point2.X - point.X) + (point2.Y - point.Y) * (point2.Y - point.Y);
        }

        public static bool IsBetweenTwoPoints(this Point targetPoint, Point point1, Point point2)
        {
            double minX = Math.Min(point1.X, point2.X);
            double minY = Math.Min(point1.Y, point2.Y);
            double maxX = Math.Max(point1.X, point2.X);
            double maxY = Math.Max(point1.Y, point2.Y);

            double targetX = targetPoint.X;
            double targetY = targetPoint.Y;

            return minX <= (targetX) && targetX <= (maxX) && minY <= (targetY) && targetY <= (maxY);
        }

        public static Point GetMidPoint(this Point point1, Point point2)
        {
            double XDiff = point2.X - point1.X;
            double YDiff = point2.Y - point1.Y;

            return new Point(point1.X + XDiff / 2, point1.Y + YDiff / 2);
        }

        public static Point Offset(this Point myPoint, Point otherPoint)
        {
            Point offsetPoint = myPoint;
            offsetPoint.Offset(otherPoint.X, otherPoint.Y);
            return offsetPoint;
        }

        public static Point Offset(this Point myPoint, double distance)
        {
            Point offsetPoint = myPoint;
            offsetPoint.Offset(distance, distance);
            return offsetPoint;
        }

        /// <summary>
        ///  converts the relative positions of two points into their absolute equivalents
        ///  that is the points lie at the corners of a rectangle whose origin is 0,0
        /// </summary>

        public static List<Point> GetAbsolutePoints(Point A, Point B)
        {
            Rect Area = new Rect(A, B);

            // SourceA and SourceB are the opposite corners of a rectangle
            List<Point> Points = new List<Point>()
                                   {
                                       new Point(A.X  <  B.X ? 0d : Area.Width, A.Y  <  B.Y ? 0d : Area.Height ),
                                       new Point(A.X  >  B.X ? 0d : Area.Width, A.Y  > B.Y ? 0d : Area.Height),
                };

            return Points;
        }
    }
}