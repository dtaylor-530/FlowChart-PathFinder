using System;
using System.Collections.Generic;

using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AnimationPathWpf
{
    public class AnimationPathControl : Control
    {
        public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register("Diameter", typeof(double), typeof(AnimationPathControl), new PropertyMetadata(100d, DiameterChanged));

        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(double), typeof(AnimationPathControl), new PropertyMetadata(50d, SpeedChanged));

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(object), typeof(AnimationPathControl), new PropertyMetadata(null, PathChanged));



        private Canvas contentGrid;
        private static readonly string m_PointData = "M244.5,98.5 L273.25,93.75 C278.03113,96.916667 277.52785,100.08333 273.25,103.25 z";

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            contentGrid = GetTemplateChild("PATH_ContentGrid") as Canvas;
            contentGrid.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }


        public object Path
        {
            get { return (object)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }


        public double Speed
        {
            get { return (double)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }



        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diameter.  This enables animation, styling, binding, etc...

        private static void DiameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).DiameterChanges.OnNext((double)e.NewValue);
        }

        private static void SpeedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).SpeedChanges.OnNext((double)e.NewValue);
        }

        private static void PathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).PathChanges.OnNext(e.NewValue);
        }

        ISubject<double> DiameterChanges = new Subject<double>();
        ISubject<double> SpeedChanges = new Subject<double>();
        ISubject<object> PathChanges = new Subject<object>();

        private static readonly Storyboard _storyboard = new Storyboard();
        private static readonly Random rd = new Random();

        public AnimationPathControl()
        {
            Uri resourceLocater = new Uri("/AnimationPathWpf;component/Themes/AnimationPathControl.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["AnimationPathControlStyle"] as Style;

   
            PathChanges
                .Throttle(TimeSpan.FromSeconds(1))
                .ObserveOnDispatcher()
                .CombineLatest(DiameterChanges.StartWith(10), SpeedChanges.StartWith(10), (p, dia, speed) =>
               {
                   Point start;
                   Point end;
                   PathGeometry geometry = p as PathGeometry;
                 
                       start = (geometry).Figures.ElementAt(0).StartPoint;
                       end = (geometry).Figures.ElementAt((geometry).Figures.Count-1).StartPoint;
                   end = new Point(end.X + geometry.Bounds.Width, end.Y + geometry.Bounds.Height);
                   //var geometry = AnimationPathWpf.PathEllipse.GetParticlePathGeometry(start, end, angle);
                   
                   double l = geometry.GetLength();
                   if (l != 0)
                   {
                       byte[] rgb = new byte[] { (byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255) };
                       var path = AnimationPathWpf.PathEllipse.GetParticlePath(start, end, rgb, _storyboard, geometry, l);

                       var csdd = AnimationPathWpf.PathEllipse.GetAnimation(start, end, dia, geometry, l, rgb, _storyboard, speed, m_PointData).ToList();
                       csdd.Add(path);

                       return csdd.ToArray();
                   }
                   return null;
               })
               .Where(_=>_!=null)
                .Subscribe(_ =>
                {
                    var collection = contentGrid?.Children;
                    if (collection != null)
                    {
                        foreach (var child in collection.Cast<UIElement>().ToArray())
                            contentGrid.Children.Remove(child);
                        foreach (var x in _)
                            try
                            {
                                contentGrid.Children.Add((UIElement)x);
                            }
                            catch (Exception e)
                            {
                            }
                        _storyboard.Begin(this);
                    }
                });
        }


    }
}

