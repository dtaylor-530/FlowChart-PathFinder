using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

/// <summary>
///  Move this to a singleton pattwern
/// </summary>

namespace PathFinderLibrary
{
    public static class PathCalculator
    {
        public static List<Point> FindPath(Line lineA, Line lineB, PathLine PathLine = PathLine.Orthogonal, double angle = 0)
        {
            List<Point> linkPoints = new List<Point>();
            linkPoints.Add(lineA.Point);

            switch (PathLine)
            {
                case PathLine.Straight:
                    break;

                case PathLine.Curved:
                    break;

                case PathLine.Orthogonal:
                    DetermineOrthogonalPoints(lineA, lineB, ref linkPoints);
                    break;
            }

            linkPoints.Add(lineB.Point);
            if (angle != 0)
            {
                IEnumerable<Point> bpoints = null;
                switch (PathLine)
                {
                    case PathLine.Straight:
                        break;

                    case PathLine.Curved:
                        double radius = DetermineArcRadius(lineA.Point, lineB.Point, angle);
                        bpoints = GetArcGeometry(lineA.Point, lineB.Point, radius).ToPointsList();
                        linkPoints.Clear();
                        break;

                    case PathLine.Orthogonal:
                        bpoints = GetBezierGeometry(linkPoints).ToPointsList();
                        linkPoints.Clear();
                        break;
                }
                if (bpoints != null)
                    foreach (var point in bpoints)
                        linkPoints.Add(point);
            }
            return linkPoints;
        }

        private static void DetermineOrthogonalPoints(Line lineA, Line lineB, ref List<Point> linkPoints)
        {
            Line[] primarySet = new Line[2];
            Line[] secondarySet = new Line[2];

            primarySet[0] = lineA;
            primarySet[1] = lineB;

            secondarySet[0] = new Line { Point = new Point(primarySet[0].Point.X, primarySet[1].Point.Y), Vector = new Vector(0, 0) };
            secondarySet[1] = new Line { Point = new Point(primarySet[1].Point.X, primarySet[0].Point.Y), Vector = new Vector(0, 0) };

            Vector CombinedVector = Vector.Add(primarySet[0].Vector, primarySet[1].Vector);

            //double angle = Vector.AngleBetween(CombinedVector, new Vector(1, 0));

            /*
            'The velocity of object 2 relative to object 1 is given by v:=v2−v1v:=v2−v1.'
            (N.B in our case direction is equivalent to velocity)*/
            Vector PrimaryDifferenceVector = Vector.Subtract(primarySet[0].Vector, primarySet[1].Vector);

            //The displacement of object 2 from object 1 is given by d:=p2−p1d:=p2−p1.
            Vector PrimaryDisplacementVector = Vector.Subtract((Vector)primarySet[0].Point, (Vector)primarySet[1].Point);

            // dot product of the difference and the displacement vectors for the primary vectors ( i.e the ones representing start and end) :
            //If the result is positive, then the objects are moving away from each other.
            //If the result is negative, then the objects are moving towards each other.
            //If the result is 0, then the distance is (at that instance) not changing.
            PrimaryDisplacementVector.Normalize(); //neccessary for calculating vectorlength
            double RelativeMovement = PrimaryDisplacementVector.DotProduct(PrimaryDifferenceVector);

            Vector vp = Vector.Add(PrimaryDifferenceVector, PrimaryDisplacementVector);

            double primaryvectorlength = vp.Length;

            // Primary Loop

            foreach (Line primaryVertex in primarySet)
            {
                // direction vector of primary vertex
                Vector directionVector = primaryVertex.Vector;

                foreach (Line secondaryVertex in secondarySet)
                {
                    // displacement vector between the primary and secondary vertex
                    Vector displacementVector = Vector.Subtract((Vector)primaryVertex.Point, (Vector)secondaryVertex.Point);
                    displacementVector.Normalize();

                    // type of line depends of relative direction of primary vertices:
                    // if CombinedVector has a length(magnitude), line is basic right angle e.g
                    // |__  __|
                    // else if CombinedVector is has no length(magnitude), the direction of primary vertices are opposed: line is double right angle e.g
                    // |__      __|
                    //    | or |

                    // check relation of primary vertices to one another with combined vector:
                    // direction is perpindicular
                    if (CombinedVector.X != 0 && CombinedVector.Y != 0)
                    {
                        // for whatever reason the number 2.2  for the Primary Vector Length determines whether the parrallel or perpindular line
                        // can be used
                        // this is related to the fact that if the direction of the primary vertices are facing inwards they will make any combined vector with
                        // the displacement vector smaller. Possible related to the fact that 2.2 sqrt is 1.5
                        if (primaryvectorlength > 2.2)
                        {
                            // check if parallel
                            if (!directionVector.IsPerpindicular(displacementVector))
                                if (!linkPoints.Contains(secondaryVertex.Point)) linkPoints.Add(secondaryVertex.Point);
                        }
                        else if (primaryvectorlength < 2.2)
                        {
                            // check if perpindcular
                            if (directionVector.IsPerpindicular(displacementVector))
                                if (!linkPoints.Contains(secondaryVertex.Point)) linkPoints.Add(secondaryVertex.Point);
                        }
                    }
                    // direction is parrallel & same direction
                    else if ((CombinedVector.X == 0 & CombinedVector.Y != 0) || (CombinedVector.Y == 0 & CombinedVector.X != 0))
                    {
                        if (directionVector == displacementVector.GetNormalized())
                            if (!linkPoints.Contains(secondaryVertex.Point)) linkPoints.Add(secondaryVertex.Point);
                    }
                    //  direction is parrallel but different directions
                    else if (CombinedVector.X == 0 & CombinedVector.Y == 0)
                    {
                        // Add parrallel line midpoint

                        // the RelativeMovement determines the type of midpoint selected:
                        // if >0 then midpoint taken from line running parrallel with direction, else <0 then  perpindiclar

                        if (RelativeMovement > 0)
                            // check if parrallel
                            if (!directionVector.IsPerpindicular(displacementVector))
                                linkPoints.Add(primaryVertex.Point.GetMidPoint(secondaryVertex.Point));
                        // Add perpindicular line midpoint
                        // if line can not be found - with the same direction to that of the primary vertex -
                        // to connect primary and secondary point  then  midpoint of perpindicular line is added

                        if (RelativeMovement < 0)
                            // check if perpindcular
                            if (directionVector.IsPerpindicular(displacementVector))
                                linkPoints.Add(primaryVertex.Point.GetMidPoint(secondaryVertex.Point));
                    }
                }
            }
        }

        public static List<Point> ToPointsList(this PathGeometry geometry)
        {
            List<Point> outpoints = new List<Point>();
            for (int i = 1; i < 101; i++)
            {
                geometry.GetPointAtFractionLength(((double)i) / 100, out Point xd, out Point t);
                outpoints.Add(xd);
            }
            return outpoints;
        }

        //CaliDiagramAndRaft
        public static PathGeometry GetBezierGeometry(List<Point> points)
        {
            var myPathFigure = new PathFigure { StartPoint = points.FirstOrDefault() };
            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();

            BezierSegment segment = null;

            if (points.Count == 3)

                segment = new BezierSegment
                {
                    Point1 = points[1],
                    Point2 = points[1],
                    Point3 = points[2],
                };
            else if (points.Count == 4)
            {
                segment = new BezierSegment
                {
                    Point1 = points[1],
                    Point2 = points[2],
                    Point3 = points[3],
                };
            }
            myPathSegmentCollection.Add(segment);

            myPathFigure.Segments = myPathSegmentCollection;

            var myPathFigureCollection = new PathFigureCollection { myPathFigure };

            return new PathGeometry { Figures = myPathFigureCollection };
        }

        private static Point GetPoint(Vector a, Vector b) => new Point(a.X * b.X, a.Y * b.Y);

        public static PathGeometry GetArcGeometry(Point start, Point end, double r) =>
         new PathGeometry
         {
             Figures = new PathFigureCollection
                 (
                     new[]
                     {
                        new PathFigure
                        {
                            StartPoint=start,
                            Segments=new PathSegmentCollection(
                                new []{
                                    new ArcSegment
                                    {
                                        SweepDirection=SweepDirection.Clockwise,
                                        Point=end,
                                        Size=new Size(r, r)
                                    }
                                })
                        }
                     }
                     )
         };

        public static double DetermineArcRadius(Point start, Point end, double angle)
        {
            double sinA = Math.Sin(Math.PI * angle / 180d);

            double x = start.X - end.X;
            double y = start.Y - end.Y;
            double aa = x * x + y * y;
            double l = Math.Sqrt(aa);
            return l / (sinA * 2);
        }

        //var controlPoints = new List<Point>();
        //double diff = (lineB.Point.Y - lineA.Point.X) / 3.0;
        //double xDiff = Math.Abs(lineA.Point.X - lineB.Point.X);
        //double yDiff = Math.Abs(lineA.Point.Y - lineB.Point.Y);
        //double dist = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        //double xOffset = dist / 2;
        //double yOffset = dist / 2;
        //Vector a = new Vector(xOffset, yOffset);

        //Point pt2 = GetPoint(a, lineA.Vector);
        //pt2 = new Point(lineA.Point.X + diff, lineA.Point.Y);
        //Point pt3 = GetPoint(a, lineB.Vector);
        //pt3 = new Point(lineB.Point.X - diff, lineB.Point.Y);

        //  public static PathLine PathLine { get; set; }

        //private static PathCalculator instance;

        //private PathCalculator() { }

        //public static PathCalculator Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new PathCalculator();
        //        }
        //        return instance;
        //    }
        //}
        ////public PathFinderPoints(PathLine pathLine)
        ////{
        ////    PathLine = pathLine;
        ////}
    }
}