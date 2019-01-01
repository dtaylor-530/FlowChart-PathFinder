using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PathFinderLibrary
{
    public class ConnectionPointToLineConverter : IMultiValueConverter
    {
        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    IConnectionPoint x = value as IConnectionPoint;
        //    return new Line
        //    {
        //        Vector = x.Side.ToVector(),
        //        Point = x.Position
        //    };
        //}

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            var vector = (Vector)values[0];
            var point = (Point)values[1];
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
