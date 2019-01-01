using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace System.Windows
{

    public static class VectorExtension
    {
        /// <summary>
        /// checks if vectors have same direction as unit vector, unitV.
        /// unitV nust be unit vector
        /// </summary>
        /// <param name="V"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static bool SameDirection(this Vector unitV, Vector V2)
        {
            V2.Normalize();
            return unitV == V2;
        }


        /// <summary>
        /// returns normalized version of vector
        /// </summary>
        /// <param name="V"></param>
        /// <returns></returns>
        public static Vector GetNormalized(this Vector V)
        {
            V.Normalize();
            return V;

        }


        /// <summary>
        /// checks the dotproduct = 0 (which amounts to the same thing)
        /// </summary>
        /// <param name="V"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static bool IsPerpindicular(this Vector V, Vector V2)
        {
            return V.DotProduct(V2) == 0;
        }
        /// <summary>
        /// the dotproduct of two vectors (=v1.X*V2.X+v1.Y*V2.Y)
        /// </summary>
        /// <param name="V"></param>
        /// <param name="V2"></param>
        /// <returns></returns>
        public static double DotProduct(this Vector V1, Vector V2)
        {
            return V1.X * V2.X + V1.Y * V2.Y;
        }

        /// <summary>
        /// The angle made by a combined/magnitude vector with the horizontal axis
        /// </summary>
        /// <param name="v"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double CombinedAngle(this Vector v, Vector v2)
        {
            Vector vector = Vector.Add(v, v2);
            Vector horizontalunitVector = new Vector();
            // double length = v2.Length;
            //double ab = Vector.AngleBetween(v, v2);
            return Vector.AngleBetween(vector, horizontalunitVector);
        }


    }



}
