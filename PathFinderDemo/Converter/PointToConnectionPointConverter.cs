using GeometryLibrary;
using PathFinderLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PathFinderDemo
{

    public class PointToConnectionPointConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            if (values.All(_ => _ != DependencyProperty.UnsetValue && _!=null))
            {
                if (values.Length == 2)
                {
                    return new ConnectionPoint(new Point((double)values[0], (double)values[1])) { Side = Side.Left };
                }
                else if (values.Length == 3)
                    return new ConnectionPoint(new Point((double)values[0], (double)values[1])) { Side = ((Side)values[2]) };
                else
                    return null;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }




}
