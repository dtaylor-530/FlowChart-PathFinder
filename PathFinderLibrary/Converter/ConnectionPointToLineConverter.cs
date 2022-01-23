using GeometryLibrary;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PathFinderLibrary
{
    public class ConnectionPointToLineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return CreateLine((Vector)values[0], (Point)values[1]);
        }

        public static Line CreateLine(Vector vector, Point point)
        {
            return new Line
            {
                Vector = vector,
                Point = point
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}