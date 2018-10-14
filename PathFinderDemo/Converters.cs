using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace PathFinderDemo
{
   [ValueConversion(typeof(List<Point>), typeof(PathSegmentCollection))]
    public class ConnectionPathConverter: IValueConverter
    {
        static ConnectionPathConverter()
        {
            Instance = new ConnectionPathConverter();
        }

        public static ConnectionPathConverter Instance
        {
            get;
            private set;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<Point> points = (List<Point>)value;
            PointCollection pointCollection = new PointCollection();
            foreach (Point point in points)
            {
                pointCollection.Add(point);
            }
            return pointCollection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



    [ValueConversion(typeof(object), typeof(string))]
    public class StringConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : value.ToString();
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}



