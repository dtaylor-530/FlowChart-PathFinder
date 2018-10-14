using GeometryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PathFinderLibrary
{
    public class PositionalElement
    {

        private readonly double seperation;

        //public double Radius
        //{
        //    get;set;


        //}

        public virtual double Left
        {
            get
            {
                return Position.X;
            }
            set
            {
                if (Position.X != value)
                {
                    //Position.X = value;
                    ////NotifyChanged("Left");
                }
            }
        }

        public virtual double Top
        {
            get
            {
                return Position.Y;
            }
            set
            {
                if (Position.Y != value)
                {
                    //Position.Y = value;
                    //NotifyChanged("Top");
                }
            }
        }

        private BindablePoint _size;
        public BindablePoint Size
        {
            get { return _size ?? (_size = new BindablePoint()); }
            set { _size = value; }
        }


        //public double Size { get; set; }

        public Point Position { get; set; }

        public Point TipPosition(Side side)
        {
            ////double seperation = (containerSize - itemSize) / 2.0;
            switch (side)
            {
                case Side.Left:
                    return new Point(this.Left + seperation, this.Top + Size.Y / 2.0);
                case Side.Top:
                    return new Point(this.Left + Size.X / 2.0, this.Top + seperation);
                case Side.Right:
                    return new Point(this.Left + Size.X - seperation, this.Top + Size.Y / 2.0);
                case Side.Bottom:
                    return new Point(this.Left + Size.X / 2.0, this.Top + Size.Y - seperation);

            }
            return new Point(this.Left + Size.X / 2.0, this.Top + Size.Y / 2.0);
        }



    }
}
