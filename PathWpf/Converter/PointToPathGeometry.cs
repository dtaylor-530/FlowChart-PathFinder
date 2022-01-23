using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace AnimationPathWpf
{
    [ValueConversion(typeof(Point[]), typeof(Geometry))]
    public class PointsToPathGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            IList<Point> Points = null;
            if (value is IList<Point> points)
            {
                Points = points;
            }
            else if (value is IEnumerable<Point> epoints)
            {
                Points = epoints.ToArray();
            }
            else if (value is IEnumerable<double> enumerable)
            {
                List<Point> lpoints = new List<Point>();
                double? x = default;
                foreach (var item in enumerable)
                {
                    if (x.HasValue)
                    {
                        lpoints.Add(new Point(x.Value, item));
                        x = default;
                    }
                    else
                        x = item;
                }
                Points = lpoints;
            }

            if (Points.Count > 0)
            {
                Point start = Points[0];
                List<LineSegment> segments = new List<LineSegment>();
                for (int i = 1; i < Points.Count; i++)
                {
                    segments.Add(new LineSegment(Points[i], true));
                }
                PathFigure figure = new PathFigure(start, segments, false); //true if closed
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                return geometry;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(Point[]), typeof(Geometry))]
    public class DoublesToPathGeometryConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            IList<Point> Points = null;
            if (value.OfType<Point>() is IEnumerable<Point> points)
            {
                Points = points.ToArray();
            }
            if (value.OfType<double>() is IEnumerable<double> enumerable)
            {
                List<Point> lpoints = new List<Point>();
                double? x = default;
                foreach (var item in enumerable)
                {
                    if (x.HasValue)
                    {
                        lpoints.Add(new Point(x.Value, item));
                        x = default;
                    }
                    else
                        x = item;
                }
                Points = lpoints;
            }

            if (Points.Count > 0)
            {
                Point start = Points[0];
                List<LineSegment> segments = new List<LineSegment>();
                for (int i = 1; i < Points.Count; i++)
                {
                    segments.Add(new LineSegment(Points[i], true));
                }
                PathFigure figure = new PathFigure(start, segments, false); //true if closed
                PathGeometry geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                return geometry;
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}