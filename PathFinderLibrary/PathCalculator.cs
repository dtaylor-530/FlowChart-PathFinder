
using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;



/// <summary>
///  Move this to a singleton pattwern
/// </summary>


namespace PathFinderLibrary
{



    public class PathCalculator
    {

        public static PathLine PathLine { get; set; }



        private static PathCalculator instance;

        private PathCalculator() { }

        public static PathCalculator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PathCalculator();
                }
                return instance;
            }
        }



        ////public PathFinderPoints(PathLine pathLine)
        ////{
        ////    PathLine = pathLine;


        ////}

        public static List<Point> FindPath(IConnectionPoint vertexA, IConnectionPoint vertexB)
        {
            List<Point> linkPoints = new List<Point>();
            linkPoints.Add(vertexA.Position);
            IConnectionPoint[] vertexSet = new IConnectionPoint[4];

            switch (PathLine)
            {
                case PathLine.Straight:

                    linkPoints.Add(vertexA.Position);
                    linkPoints.Add(vertexB.Position);
                    break;
                case PathLine.Orthogonal:
                    // primary vertices
                    vertexSet[0] = vertexA;
                    vertexSet[1] = vertexB;
                    // secondary vertices
                    vertexSet[2] = new ConnectionPoint(position : new Point(vertexA.Position.X, vertexB.Position.Y)) { IsPrimary = false, Side = Side.None };
                    vertexSet[3] = new ConnectionPoint(position : new Point(vertexB.Position.X, vertexA.Position.Y)) { IsPrimary = false, Side = Side.None };
                    DetermineOrthogonalPoints(vertexSet, linkPoints);
                    linkPoints.Add(vertexB.Position);
                    break;
            }

            return linkPoints;
        }





        private static void DetermineOrthogonalPoints(IConnectionPoint[] VertexSet, List<Point> linkPoints)
        {



            Vector CombinedVector = Vector.Add(VertexSet[0].Side.ToVector(), VertexSet[1].Side.ToVector());

            //double angle = Vector.AngleBetween(CombinedVector, new Vector(1, 0));

            /*
            'The velocity of object 2 relative to object 1 is given by v:=v2−v1v:=v2−v1.'
            (N.B in our case direction is equivalent to velocity)*/
            Vector PrimaryDifferenceVector = Vector.Subtract(VertexSet[0].Side.ToVector(), VertexSet[1].Side.ToVector());

            //The displacement of object 2 from object 1 is given by d:=p2−p1d:=p2−p1.
            Vector PrimaryDisplacementVector = Vector.Subtract((Vector)VertexSet[0].Position, (Vector)VertexSet[1].Position);

            // dot product of the difference and the displacement vectors for the primary vectors ( i.e the ones representing start and end) : 
            //If the result is positive, then the objects are moving away from each other.
            //If the result is negative, then the objects are moving towards each other. 
            //If the result is 0, then the distance is (at that instance) not changing.
            PrimaryDisplacementVector.Normalize(); //neccessary for calculating vectorlength
            double RelativeMovement = PrimaryDisplacementVector.DotProduct(PrimaryDifferenceVector);


            Vector vp = Vector.Add(PrimaryDifferenceVector, PrimaryDisplacementVector);

            double primaryvectorlength = vp.Length;


            // Primary Loop

            foreach (IConnectionPoint primaryVertex in VertexSet.Where(x => x.IsPrimary == true))
            {

                // direction vector of primary vertex
                Vector directionVector = primaryVertex.Side.ToVector();


                foreach (IConnectionPoint secondaryVertex in VertexSet.Where(x => x.IsPrimary == false))
                {
                    // displacement vector between the primary and secondary vertex
                    Vector displacementVector = Vector.Subtract((Vector)primaryVertex.Position, (Vector)secondaryVertex.Position);
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
                            // check if parrallel
                            if (!directionVector.IsPerpindicular(displacementVector))
                            {
                                if (!linkPoints.Contains(secondaryVertex.Position)) linkPoints.Add(secondaryVertex.Position);
                            }
                        }
                        else if (primaryvectorlength < 2.2)
                        {
                            // check if perpindcular
                            if (directionVector.IsPerpindicular(displacementVector))
                                if (!linkPoints.Contains(secondaryVertex.Position)) linkPoints.Add(secondaryVertex.Position);
                        }

                    }
                    // direction is parrallel & same direction
                    else if ((CombinedVector.X == 0 & CombinedVector.Y != 0) || (CombinedVector.Y == 0 & CombinedVector.X != 0))
                    {
                        if (directionVector == displacementVector.GetNormalized())
                        {
                            if (!linkPoints.Contains(secondaryVertex.Position)) linkPoints.Add(secondaryVertex.Position);

                        }

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
                            {
                                linkPoints.Add(primaryVertex.Position.GetMidPoint(secondaryVertex.Position));
                            }
                        // Add perpindicular line midpoint
                        // if line can not be found - with the same direction to that of the primary vertex - 
                        // to connect primary and secondary point  then  midpoint of perpindicular line is added

                        if (RelativeMovement < 0)
                        {
                            // check if perpindcular
                            if (directionVector.IsPerpindicular(displacementVector))
                                linkPoints.Add(primaryVertex.Position.GetMidPoint(secondaryVertex.Position));
                        }
                    }
                }
            }
        }
    }

}
