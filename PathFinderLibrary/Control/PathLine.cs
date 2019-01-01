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
    public class PathPolyLine : Control, IObserver<double>
    {
        private static PathLine pathLine = PathLine.Straight;
        public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register("StartPoint", typeof(object), typeof(PathPolyLine), new PropertyMetadata(null, (d, e) => (d as PathPolyLine).UpdateConnectionPoints0()));

        public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register("EndPoint", typeof(object), typeof(PathPolyLine), new PropertyMetadata(null, (d, e) => (d as PathPolyLine).UpdateConnectionPoints0()));

        public static readonly DependencyProperty PathLineProperty = DependencyProperty.Register("PathLine", typeof(PathLine), typeof(PathPolyLine), new PropertyMetadata(pathLine, PathLineChanged));

        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(PathPolyLine), new PropertyMetadata(0d, AngleChanged));

        public static readonly DependencyProperty ConnectionPointsProperty = DependencyProperty.Register("ConnectionPoints", typeof(PointCollection), typeof(PathPolyLine), new PropertyMetadata(null));



        Subject<double> AngleChanges = new Subject<double>();


        ConnectionPointToLineConverter converter = new ConnectionPointToLineConverter();

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

        private static void AngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathPolyLine).AngleChanges.OnNext((double)e.NewValue);
        }


        private static void PathLineChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as PathPolyLine).UpdateConnectionPoints0();
        }


        static PathPolyLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathPolyLine), new FrameworkPropertyMetadata(typeof(PathPolyLine)));
        }

        //public override void OnApplyTemplate()
        //{
        //    //base.OnApplyTemplate();
        //    //PathFinderLine = GetTemplateChild("PolyLine") as Polyline;
        //}

        public PathPolyLine()
        {
            AngleChanges = new Subject<double>();
            AngleChanges.Subscribe(this);
        }


        private void UpdateConnectionPoints0()
        {
            var startConnection = (IConnectionPoint)StartPoint;
            var endConnection = (IConnectionPoint)EndPoint;
            if (startConnection != null && endConnection != null)
            {
                List<Point> points = null;

                //if (Angle>0)
                //    points = GetPathAngle(startConnection, endConnection);
                //else
                    points = GetPath(startConnection, endConnection);


                PointCollection pointCollection = new PointCollection();
                foreach (Point point in points)
                    pointCollection.Add(point);

                this.Dispatcher.InvokeAsync(() =>
                {
                    ConnectionPoints = pointCollection;
                },
                System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
            }
        }

        List<Point> GetPath(IConnectionPoint startConnection, IConnectionPoint endConnection) =>
           PathCalculator.FindPath(
               (GeometryLibrary.Line)converter.Convert(new object[] { startConnection.Side.ToVector(), startConnection.Position }, null, null, null),
               (GeometryLibrary.Line)converter.Convert(new object[] { endConnection.Side.ToVector(), endConnection.Position }, null, null, null),
               PathLine,Angle);



        List<Point> GetPathAngle(IConnectionPoint startConnection, IConnectionPoint endConnection)
        {
            //var _startConnection = (IConnectionPoint)StartPoint;
            //var _endConnection = (IConnectionPoint)EndPoint;

            // displacement vector between the primary and secondary vertex
            Vector displacementVector = Vector.Subtract((Vector)startConnection.Position, (Vector)endConnection.Position);
            //displacementVector.Normalize();
            var startLine = (GeometryLibrary.Line)converter.Convert(new object[] { displacementVector, startConnection.Position }, null, null, null);
            var endLine = (GeometryLibrary.Line)converter.Convert(new object[] { -displacementVector, endConnection.Position }, null, null, null);

           return PathCalculator.FindPath(
           (GeometryLibrary.Line)converter.Convert(new object[] { startConnection.Side.ToVector(), startConnection.Position }, null, null, null),
           (GeometryLibrary.Line)converter.Convert(new object[] { endConnection.Side.ToVector(), endConnection.Position }, null, null, null),
           PathLine);


        }


        public void OnNext(double value)
        {
            UpdateConnectionPoints0();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
