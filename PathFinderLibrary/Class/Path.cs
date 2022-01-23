namespace PathFinderLibrary
{
    //public class Path : INotifyPropertyChanged
    //{
    //    private Rect area;
    //    private ConnectionPoint sourceTip;
    //    private ConnectionPoint sinkTip;

    //    public Point StartPoint=> WayPoints[0];

    //    public Point EndPoint => WayPoints.Last();

    //    public Point MiddlePoint=> WayPoints[(WayPoints.Count / 2)];

    //    ConnectionPointToLineConverter  converter = new ConnectionPointToLineConverter();

    //    public List<Point> GetPath() => PathCalculator.FindPath((Line)converter.Convert(sourceTip,null,null,null), (Line)converter.Convert(sinkTip,null,null,null));

    //    public void UpdateWayPoints()
    //    {
    //        if (sourceTip != null && sinkTip != null)
    //        {
    //            sourceTip.Side = SideExtension.RelativeSide(sourceTip.Position, sinkTip.Position);
    //            sinkTip.Side = SideExtension.RelativeSide(sinkTip.Position, sinkTip.Position);
    //            WayPoints = GetPath();
    //        }
    //    }

    //    #region INotifyPropertyChanged Implementation
    //    /// <summary>
    //    /// Occurs when any properties are changed on this object.
    //    /// </summary>
    //    public event PropertyChangedEventHandler PropertyChanged;

    //    /// <summary>
    //    /// A helper method that raises the PropertyChanged event for a property.
    //    /// </summary>
    //    /// <param name="propertyNames">The names of the properties that changed.</param>
    //    public virtual void NotifyChanged(params string[] propertyNames)
    //    {
    //        foreach (string name in propertyNames)
    //        {
    //            OnPropertyChanged(new PropertyChangedEventArgs(name));
    //        }
    //    }

    //    /// <summary>
    //    /// Raises the PropertyChanged event.
    //    /// </summary>
    //    /// <param name="e">Event arguments.</param>
    //    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    //    {
    //        if (this.PropertyChanged != null)
    //        {
    //            this.PropertyChanged(this, e);
    //        }
    //    }
    //    #endregion

    //    public Path(ConnectionPoint sourceTip, ConnectionPoint sinkTip) //: base((Point)(sourceTip.Position - sinkTip.Position))
    //    {
    //        SourceTip = sourceTip;
    //        SinkTip = sinkTip;
    //        Init();
    //    }

    //    private void Init()
    //    {
    //        UpdateWayPoints();
    //    }

    //    public bool IsFullConnection
    //    {
    //        get { return (sinkTip.Target != null && sourceTip.Target != null); }
    //    }

    //    private List<Point> wayPoints;

    //    public List<Point> WayPoints
    //    {
    //        get
    //        {
    //            return wayPoints;
    //        }
    //        private set
    //        {
    //            if (wayPoints != value)
    //            {
    //                wayPoints = value;
    //                NotifyChanged("WayPoints");
    //            }
    //        }
    //    }

    //    public ConnectionPoint SourceTip
    //    {
    //        get
    //        {
    //            return sourceTip;
    //        }
    //        set
    //        {
    //            if (sourceTip != value)
    //            {
    //                sourceTip = value;
    //                sourceTip.PropertyChanged += SourceTip_PropertyChanged;
    //               // (sourceTip as INotifyPropertyChanged).PropertyChanged +=new WeakINPCEventHandler(SourceTip_PropertyChanged).Handler;
    //                NotifyChanged("SourceTip");
    //            }
    //        }
    //    }

    //    private void SourceTip_PropertyChanged1(object sender, PropertyChangedEventArgs e)
    //    {
    //        //throw new NotImplementedException();
    //    }

    //    public ConnectionPoint SinkTip
    //    {
    //        get
    //        {
    //            return sinkTip;
    //        }
    //        set
    //        {
    //            if (sinkTip != value)
    //            {
    //                sinkTip = value;
    //                sinkTip.PropertyChanged += SinkTip_PropertyChanged;
    //                //  (sinkTip as INotifyPropertyChanged).PropertyChanged += new WeakINPCEventHandler(SinkTip_PropertyChanged).Handler;
    //                NotifyChanged("SinkTip");
    //            }
    //        }
    //    }

    //    private void UpdateConnectionPoints0()
    //    {
    //        WayPoints = GetPath();

    //    }
    //    //private List<Point> Points;

    //        private void SourceTip_PropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (e.PropertyName == "Position")
    //            UpdateWayPoints();
    //        if (e.PropertyName == "Orientation") ;
    //        // pathFinder.FindPath(sinkTip, sourceTip);
    //        // UpdateConnectionPoints0();

    //    }

    //    private void SinkTip_PropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        if (e.PropertyName == "Position")
    //            UpdateWayPoints();
    //        else if (e.PropertyName == "Orientation") ;
    //        //  UpdateConnectionPoints0();
    //        else if (e.PropertyName == "DesignerItemTip") ;
    //            //base.IsCompleted = (sender as ConnectorTipViewModel).Target != null;
    //    }

    //}
}