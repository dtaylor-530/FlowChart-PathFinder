using System;
using System.ComponentModel;
using System.Windows;

namespace PathFinderLibrary
{
    public class BindablePoint : INotifyPropertyChanged
    {
        public double X
        {
            get { return Value.X; }
            set { Value = new Point(value, Value.Y); }
        }

        public double Y
        {
            get { return Value.Y; }
            set { Value = new Point(Value.X, value); }
        }

        private Point _value;

        public Point Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifyChanged("Value");
                NotifyChanged("X");
                NotifyChanged("Y");

                if (ValueChanged != null)
                    ValueChanged();
            }
        }

        public Action ValueChanged;

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

        #endregion INotifyPropertyChanged Implementation
    }
}