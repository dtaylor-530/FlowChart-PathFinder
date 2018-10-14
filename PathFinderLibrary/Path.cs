using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PathFinderLibrary
{

    //public class PathControl : FrameworkElement
    //{
    //    static PathControl()
    //    {
    //        ClipToBoundsProperty.OverrideMetadata(typeof(PathControl), new FrameworkPropertyMetadata(true));
    //    }

    //    public PathControl()
    //    {
       
    //    }


    //    public Point StartPoint
    //    {
    //        get { return (Point)GetValue(StartPointProperty); }
    //        set { SetValue(StartPointProperty, value); }
    //    }
    //    public static readonly DependencyProperty StartPointProperty = DependencyProperty.Register(
    //        "StartPoint", typeof(Point), typeof(PathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(StartPointPropertyChanged)));

    //    private static void StartPointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //       // throw new NotImplementedException();
    //    }


    //    public Point EndPoint
    //    {
    //        get { return (Point)GetValue(EndPointProperty); }
    //        set { SetValue(EndPointProperty, value); }
    //    }
    //    public static readonly DependencyProperty EndPointProperty = DependencyProperty.Register(
    //        "EndPoint", typeof(Point), typeof(PathControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(EndPointPropertyChanged)));

    //    private static void EndPointPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        // throw new NotImplementedException();
    //    }




    //    //private static void CenterObjectPropertyChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
    //    //{
    //    //    ((Graph)element).CenterObjectPropertyChanged();
    //    //}

    //    //private void CenterObjectPropertyChanged()
    //    //{
    //    //    m_centerChanged = true;
    //    //    resetNodesBinding();
    //    //}



    //}






    public class Path : INotifyPropertyChanged
    {



        //public ConnectionPoint sourceTip { get; set; }
        //public ConnectionPoint sinkTip { get; set; }
        private ConnectionPoint sourceTip;
        private ConnectionPoint sinkTip;

        public List<Point> GetPath()
        {

            return PathCalculator.FindPath(sourceTip, sinkTip);

        }




        public Point StartPoint
        {
            get
            {
                return WayPoints[0];
            }

        }

        public Point EndPoint
        {
            get
            {
                return WayPoints.Last();
            }

        }


        public Point MiddlePoint
        {
            get
            {
                return WayPoints[(WayPoints.Count / 2)];
            }
        }



        //public void UpdateWayPoints2()
        //{

        //    if (sourceTip != null && sinkTip != null)
        //    {
        //        List<Point> points = GeometryLibrary.PointExtension.GetAbsolutePoints(sourceTip.RelativePosition, sinkTip.RelativePosition);

        //        sourceTip.Position = points[0];
        //        sinkTip.Position = points[1];
        //        sourceTip.Side = SideExtension.RelativeSide(sourceTip.Position, sinkTip.Position);
        //        sinkTip.Side = SideExtension.RelativeSide(sinkTip.Position, sinkTip.Position);
        //        WayPoints = GetPath();
            

        //    }
        //}




        public void UpdateWayPoints()
        {

            if (sourceTip != null && sinkTip != null)
            {

                sourceTip.Side = SideExtension.RelativeSide(sourceTip.Position, sinkTip.Position);
                sinkTip.Side = SideExtension.RelativeSide(sinkTip.Position, sinkTip.Position);
                WayPoints = GetPath();
                // NotifyChanged("WayPoints");
                //var c = WayPoints.Count;


                //// the posiiton of the connector marks the center of the id tag
                //if (Convert.ToBoolean(c % 2))
                //{
                //    MiddlePoint= WayPoints[(c / 2)];
                //}
                //else
                //{

                //    MiddlePoint = PointExtensions.GetMidPoint(ConnectionPoints[(c / 2) - 1], ConnectionPoints[(c / 2)]);
                //}

            }
        }




        #region INotifyPropertyChanged Implementation
        /// <summary>
        /// Occurs when any properties are changed on this object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// A helper method that raises the PropertyChanged event for a property.
        /// </summary>
        /// <param name="propertyNames">The names of the properties that changed.</param>
        public virtual void NotifyChanged(params string[] propertyNames)
        {
            foreach (string name in propertyNames)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, e);
            }
        }
        #endregion





        private Rect area;



        public Path(ConnectionPoint sourceTip, ConnectionPoint sinkTip) //: base((Point)(sourceTip.Position - sinkTip.Position))
        {

            SourceTip = sourceTip;
            SinkTip = sinkTip;
            Init();
        }


        private void Init()
        {

            UpdateWayPoints();


        }




        public bool IsFullConnection
        {
            get { return (sinkTip.Target != null && sourceTip.Target != null); }
        }

        private List<Point> wayPoints;

        public List<Point> WayPoints
        {
            get
            {
                return wayPoints;
            }
            private set
            {
                if (wayPoints != value)
                {
                    wayPoints = value;
                    NotifyChanged("WayPoints");
                }
            }
        }





        public ConnectionPoint SourceTip
        {
            get
            {
                return sourceTip;
            }
            set
            {
                if (sourceTip != value)
                {
                    sourceTip = value;
                    sourceTip.PropertyChanged += SourceTip_PropertyChanged;
                   // (sourceTip as INotifyPropertyChanged).PropertyChanged +=new WeakINPCEventHandler(SourceTip_PropertyChanged).Handler;
                    NotifyChanged("SourceTip");
                }
            }
        }

        private void SourceTip_PropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public ConnectionPoint SinkTip
        {
            get
            {
                return sinkTip;
            }
            set
            {
                if (sinkTip != value)
                {
                    sinkTip = value;
                    sinkTip.PropertyChanged += SinkTip_PropertyChanged;
                    //  (sinkTip as INotifyPropertyChanged).PropertyChanged += new WeakINPCEventHandler(SinkTip_PropertyChanged).Handler; 
                    NotifyChanged("SinkTip");
                }
            }
        }



        private void UpdateConnectionPoints0()
        {

            WayPoints = PathFinderLibrary.PathCalculator.FindPath(sourceTip, sinkTip);
        }


        //private List<Point> Points;


        private void SourceTip_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position")
                UpdateWayPoints();
            if (e.PropertyName == "Orientation") ;
            // pathFinder.FindPath(sinkTip, sourceTip);
            // UpdateConnectionPoints0();

        }

        private void SinkTip_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position")
                UpdateWayPoints();
            else if (e.PropertyName == "Orientation") ;
            //  UpdateConnectionPoints0();
            else if (e.PropertyName == "DesignerItemTip") ;
                //base.IsCompleted = (sender as ConnectorTipViewModel).Target != null;
        }



    }

    //[DebuggerNonUserCode]
    public sealed class WeakINPCEventHandler
    {
        private readonly WeakReference _targetReference;
        private readonly MethodInfo _method;

        public WeakINPCEventHandler(PropertyChangedEventHandler callback)
        {
            _method = callback.Method;
            _targetReference = new WeakReference(callback.Target, true);
        }

        //[DebuggerNonUserCode]
        public void Handler(object sender, PropertyChangedEventArgs e)
        {
            var target = _targetReference.Target;
            if (target != null)
            {
                ((Action<object, PropertyChangedEventArgs>)Delegate.CreateDelegate(typeof(Action<object, PropertyChangedEventArgs>), target, _method, true))?.Invoke(sender, e);
            }
        }
    }
}