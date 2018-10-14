using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PathFinderLibrary
{

    public class ConnectionPoint : IConnectionPoint,INotifyPropertyChanged
    {
        private double size;

        public ConnectionPoint(string side, double size=0, bool isPrimary = true)
        {
            Side = side.FromString();
            IsPrimary = isPrimary;
        }

        public ConnectionPoint(Point position)
        {
            Position = position;
        }

        //public Point Position { get; set; }

        private Point position;
        public Point Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                NotifyChanged("Position");
            }

        }



        //private Point relativePosition;

        //public Point RelativePosition
        //{
        //    get
        //    {
        //        return relativePosition;
        //    }

        //    set
        //    {
        //        relativePosition = value;
        //        NotifyChanged("RelativePosition");
        //    }

        //}


        public bool IsPrimary { get; set; }
        public Side Side
        {
            get; set;


        }

        private ITarget target;

        public ITarget Target
        {
            get
            {
                return target;
            }
            set
            {
               target = value;
                target.AddConnectorTip(this);
            }
        }



        private void OffsetPoint(Point point)
        {
            switch (Side)
            {
                case Side.Top:
                    point.Offset(0, -size);
                    break;
                case Side.Bottom:
                    point.Offset(0, size);
                    break;
                case Side.Right:
                    point.Offset(size, 0);
                    break;
                case Side.Left:
                    point.Offset(-size, 0);
                    break;
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

    }

    public interface ITarget
    {
        void AddConnectorTip(IConnectionPoint connectionPoint);
    }
}