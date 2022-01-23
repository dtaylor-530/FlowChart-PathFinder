using GeometryLibrary.WinForms;
using System.Collections.Generic;
using System.Windows;

public class Bezier
{
    public Point P1;   // Begin Point
    public Point P2;   // Control Point
    public Point P3;   // Control Point
    public Point P4;   // End Point

    // Made these global so I could diagram the top solution
    public Line L12;

    public Line L23;
    public Line L34;

    public Point P12;
    public Point P23;
    public Point P34;

    public Line L1223;
    public Line L2334;

    public Point P123;
    public Point P234;

    public Line L123234;
    public Point P1234;

    public Bezier(Point p1, Point p2, Point p3, Point p4)
    {
        P1 = p1; P2 = p2; P3 = p3; P4 = p4;
    }

    /// <summary>
    /// Consider the classic Casteljau diagram
    /// with the bezier points p1, p2, p3, p4 and lines l12, l23, l34
    /// and their midpoint of line l12 being p12 ...
    /// and the line between p12 p23 being L1223
    /// and the midpoint of line L1223 being P1223 ...
    /// </summary>
    /// <param name="lines"></param>
    public void SplitBezier(List<Line> lines)
    {
        lines = lines ?? new List<Line>();

        L12 = new Line(this.P1, this.P2);
        L23 = new Line(this.P2, this.P3);
        L34 = new Line(this.P3, this.P4);

        P12 = L12.MidPoint;
        P23 = L23.MidPoint;
        P34 = L34.MidPoint;

        L1223 = new Line(P12, P23);
        L2334 = new Line(P23, P34);

        P123 = L1223.MidPoint;
        P234 = L2334.MidPoint;

        L123234 = new Line(P123, P234);

        P1234 = L123234.MidPoint;

        if (CurveIsFlat())
        {
            lines.Add(new Line(this.P1, this.P4));
            return;
        }
        else
        {
            Bezier bz1 = new Bezier(this.P1, P12, P123, P1234);
            bz1.SplitBezier(lines);

            Bezier bz2 = new Bezier(P1234, P234, P34, this.P4);
            bz2.SplitBezier(lines);
        }

        return;
    }

    /// <summary>
    /// Check if points P1, P1234 and P2 are colinear (enough).
    /// This is very simple-minded algo... there are better...
    /// </summary>
    /// <returns></returns>
    public bool CurveIsFlat()
    {
        double t1 = (P2.Y - P1.Y) * (P3.X - P2.X);
        double t2 = (P3.Y - P2.Y) * (P2.X - P1.X);

        double delta = System.Math.Abs(t1 - t2);

        return delta < 0.1; // Hard-coded constant
    }
}