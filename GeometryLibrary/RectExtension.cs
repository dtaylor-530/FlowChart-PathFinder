namespace System.Windows
{
    public static class RectExtension
    {
        /// <summary>
        /// breaks rectangle into LineEquation objects starting from leftmost side and working clockwise
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        //public static IEnumerable<Line> LineSegments(this Rect rectangle)
        //{
        //    var lines = new List<Line>
        //    {
        //        new Line(new Point(rectangle.X, rectangle.Y),
        //                         new Point(rectangle.X, rectangle.Y + rectangle.Height)),

        //        new Line(new Point(rectangle.X, rectangle.Y + rectangle.Height),
        //                         new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height)),

        //        new Line(new Point(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
        //                         new Point(rectangle.X + rectangle.Width, rectangle.Y)),

        //        new Line(new Point(rectangle.X + rectangle.Width, rectangle.Y),
        //                         new Point(rectangle.X, rectangle.Y)),
        //    };

        //    return lines;

        //}

        //improved from original at http://www.codeproject.com/Tips/403031/Extension-methods-for-finding-centers-of-a-rectang

        /// <summary>
        /// Returns the center point of the rectangle
        /// </summary>
        /// <param name="r"></param>
        /// <returns>Center point of the rectangle</returns>
        public static Point Center(this Rect r)
        {
            return new Point((float)(r.Left + (r.Width / 2D)), (float)(r.Top + (r.Height / 2D)));
        }

        /// <summary>
        /// Returns the center right point of the rectangle
        /// i.e. the right hand edge, centered vertically.
        /// </summary>
        /// <param name="r"></param>
        /// <returns>Center right point of the rectangle</returns>
        public static Point CenterRight(this Rect r)
        {
            return new Point((r.Right), (float)(r.Top + (r.Height / 2D)));
            //Rect r = new Rect();
        }

        /// <summary>
        /// Returns the center left point of the rectangle
        /// i.e. the left hand edge, centered vertically.
        /// </summary>
        /// <param name="r"></param>
        /// <returns>Center left point of the rectangle</returns>
        public static Point CenterLeft(this Rect r)
        {
            return new Point(r.Left, (float)(r.Top + (r.Height / 2D)));
        }

        /// <summary>
        /// Returns the center bottom point of the rectangle
        /// i.e. the bottom edge, centered horizontally.
        /// </summary>
        /// <param name="r"></param>
        /// <returns>Center bottom point of the rectangle</returns>
        public static Point CenterBottom(this Rect r)
        {
            return new Point((float)(r.Left + (r.Width / 2D)), r.Bottom);
        }

        /// <summary>
        /// Returns the center top point of the rectangle
        /// i.e. the topedge, centered horizontally.
        /// </summary>
        /// <param name="r"></param>
        /// <returns>Center top point of the rectangle</returns>
        public static Point CenterTop(this Rect r)
        {
            return new Point((float)(r.Left + (r.Width / 2D)), r.Top);
        }
    }
}