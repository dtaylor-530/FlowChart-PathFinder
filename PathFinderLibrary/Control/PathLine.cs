using GeometryLibrary;
using PathFinderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathFinderLibrary
{
    [TemplatePart(Name = "PolyLine", Type = typeof(Polyline))]
    public class PathPolyLine : ContentControl
    {
        private static PathLine pathLine = PathLine.Straight;
        public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(object), typeof(PathPolyLine), new PropertyMetadata(null, (d, e) => (d as PathPolyLine).UpdateConnectionPoints()));
        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register("EndPoint", typeof(object), typeof(PathPolyLine), new PropertyMetadata(null, (d, e) => (d as PathPolyLine).UpdateConnectionPoints()));
        public static readonly DependencyProperty PathLineProperty = DependencyProperty.Register("PathLine", typeof(PathLine), typeof(PathPolyLine), new PropertyMetadata(pathLine, PathLineChanged));
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(PathPolyLine), new PropertyMetadata(0d, AngleChanged));
        public static readonly DependencyProperty ConnectionPointsProperty = DependencyProperty.Register("ConnectionPoints", typeof(PointCollection), typeof(PathPolyLine), new PropertyMetadata(null));


        public PathLine PathLine
        {
            get { return (PathLine)GetValue(PathLineProperty); }
            set { SetValue(PathLineProperty, value); }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public PointCollection ConnectionPoints
        {
            get { return (PointCollection)GetValue(ConnectionPointsProperty); }
            set { SetValue(ConnectionPointsProperty, value); }
        }

        public object StartPoint
        {
            get { return (object)GetValue(StartPointProperty); }
            set { SetValue(StartPointProperty, value); }
        }


        public object EndPoint
        {
            get { return (object)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        public Polyline Polyline { get; }

        private static void AngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathPolyLine).UpdateConnectionPoints();
        }
        private static void PathLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathPolyLine).UpdateConnectionPoints();
        }


        static PathPolyLine()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(PathPolyLine), new FrameworkPropertyMetadata(typeof(PathPolyLine)));
        }


        public PathPolyLine()
        {
            Polyline = new Polyline();
            Polyline.Stroke = Brushes.Gray;
 
            Polyline.StrokeThickness = 2;
       
            this.Content = Polyline;
        }

        private void UpdateConnectionPoints()
        {
            var startConnection = (IConnectionPoint)StartPoint;
            var endConnection = (IConnectionPoint)EndPoint;
            if (startConnection != null && endConnection != null)
            {
                List<Point> points = null;
           
                points = GetPoints(startConnection, endConnection);

                PointCollection pointCollection = new PointCollection();
                foreach (Point point in points)
                    pointCollection.Add(point);

                this.Dispatcher.InvokeAsync(() =>
                {
                    ConnectionPoints = pointCollection;
                    Polyline.Points = ConnectionPoints;
                },
                System.Windows.Threading.DispatcherPriority.Background, default);
            }
        }

        List<Point> GetPoints(IConnectionPoint startConnection, IConnectionPoint endConnection) =>
           PathCalculator.FindPath(
              ConnectionPointToLineConverter.CreateLine(startConnection.Side.ToVector(), startConnection.Position),
              ConnectionPointToLineConverter.CreateLine(endConnection.Side.ToVector(), endConnection.Position),
               PathLine, Angle);


    }
}
