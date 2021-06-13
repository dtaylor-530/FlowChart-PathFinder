using System;

using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AnimationPathWpf
{
    public class AnimationPathControl : Canvas
    {
        private static double speed = 50d;
        private static double diameter = 50d;
        private (bool a, bool b, bool c) testc;
        private static readonly string m_PointData = "M244.5,98.5 L273.25,93.75 C278.03113,96.916667 277.52785,100.08333 273.25,103.25 z";

        public static readonly DependencyProperty DiameterProperty = DependencyProperty.Register("Diameter", typeof(double), typeof(AnimationPathControl), new PropertyMetadata(diameter, DiameterChanged));

        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(double), typeof(AnimationPathControl), new PropertyMetadata(speed, SpeedChanged));

        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(object), typeof(AnimationPathControl), new PropertyMetadata(Geometry.Parse(m_PointData), PathChanged));

        public static readonly DependencyProperty PathStyleProperty = DependencyProperty.Register("PathStyle", typeof(Style), typeof(AnimationPathControl), new PropertyMetadata((Style)Application.Current.TryFindResource("ParticlePathStyle"), StyleChanged));

        public static readonly DependencyProperty ShowParticleProperty = DependencyProperty.Register("ShowParticle", typeof(bool), typeof(AnimationPathControl), new PropertyMetadata(true, ShowParticleChanged));

        public static readonly DependencyProperty ShowPathProperty = DependencyProperty.Register("ShowPath", typeof(bool), typeof(AnimationPathControl), new PropertyMetadata(true, ShowPathChanged));

        public static readonly DependencyProperty ShowTargetProperty = DependencyProperty.Register("ShowTarget", typeof(bool), typeof(AnimationPathControl), new PropertyMetadata(true, ShowTargetChanged));


        public bool ShowParticle
        {
            get { return (bool)GetValue(ShowParticleProperty); }
            set { SetValue(ShowParticleProperty, value); }
        }




        public bool ShowPath
        {
            get { return (bool)GetValue(ShowPathProperty); }
            set { SetValue(ShowPathProperty, value); }
        }




        public bool ShowTarget
        {
            get { return (bool)GetValue(ShowTargetProperty); }
            set { SetValue(ShowTargetProperty, value); }
        }





        readonly ISubject<double> DiameterChanges = new Subject<double>();
        readonly ISubject<double> SpeedChanges = new Subject<double>();
        readonly ISubject<Geometry> PathChanges = new Subject<Geometry>();
        readonly ISubject<Style> StyleChanges = new Subject<Style>();
        readonly ISubject<bool> ShowPathChanges = new Subject<bool>();
        readonly ISubject<bool> ShowParticleChanges = new Subject<bool>();
        readonly ISubject<bool> ShowTargetChanges = new Subject<bool>();


        private static readonly Random rd = new Random();


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


        public Style PathStyle
        {
            get { return (Style)GetValue(PathStyleProperty); }
            set { SetValue(PathStyleProperty, value); }
        }


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
            if (e.NewValue is Geometry geometry)
                (d as AnimationPathControl).PathChanges.OnNext(geometry);
            else
            {

            }
        }

        private static void StyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).StyleChanges.OnNext(e.NewValue as Style);
        }
        private static void ShowParticleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).ShowParticleChanges.OnNext((bool)e.NewValue);
        }
        private static void ShowPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).ShowPathChanges.OnNext((bool)e.NewValue);
        }

        private static void ShowTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as AnimationPathControl).ShowTargetChanges.OnNext((bool)e.NewValue);
        }

        public AnimationPathControl()
        {
            var uithread = new SynchronizationContextScheduler(SynchronizationContext.Current);

            var ada = ShowPathChanges.StartWith(ShowPath)
                .CombineLatest(
               ShowParticleChanges.StartWith(ShowParticle),
               ShowTargetChanges.StartWith(ShowTarget), (a, b, c) => (a, b, c));
                  //.ObserveOn(uithread);

            ada.Subscribe(es =>
            {
                var name = this.Name;
                testc = es;
            });

            var xs = PathChanges
               //.Buffer(TimeSpan.FromSeconds(0.5))
               //.Select(a => a.LastOrDefault())
               .Where(a => a != null)
               .CombineLatest(
               DiameterChanges.StartWith(Diameter),
               SpeedChanges.StartWith(Speed),
               StyleChanges.StartWith(Style),
               (geo, dia, speed, style) => (geo, dia, speed, style));
               //.ObserveOn(uithread);

            xs.Subscribe(a =>
            {
                var name = this.Name;
                var t = testc;
            });

            ada
                .WithLatestFrom(xs, (a, b) => (b, a))
                .Select(ac =>
              {
                  var (cd, adad) = ac;
                  var (showPathAnimation, showParticleAnimation, showTargetAnimation) = testc;
                  var (geo, dia, speed, style) = cd;

                  Point start;
                  Point end;

                  if (geo is PathGeometry geometry)
                  {
                      end = (geometry).Figures.ElementAt(0).StartPoint;
                      start = (geometry).Figures.ElementAt((geometry).Figures.Count - 1).StartPoint;
                      end = new Point(start.X + (
                          geometry.Bounds.X == start.X ?
                          geometry.Bounds.Width : -geometry.Bounds.Width),
                             geometry.Bounds.Y == start.Y ?
                             -geometry.Bounds.Height : geometry.Bounds.Height);



                      //var geometry = AnimationPathWpf.PathEllipse.GetParticlePathGeometry(start, end, angle);
                  }
                  else
                  {
                      start = geo.Bounds.TopLeft;
                      end = geo.Bounds.BottomRight;
                  }

                  double l = geo.GetLength();
                  if (l != 0)
                  {

                      double pointTime = l / speed;
                      byte[] rgb = new byte[] { (byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255) };
                      Storyboard storyBoard = new Storyboard();
                      return (PathEllipse.GetAnimations(start, end, dia, geo, rgb, storyBoard, pointTime, m_PointData, l,
                          style,
                          showPathAnimation,
                          showParticleAnimation,
                          showTargetAnimation).ToArray(), storyBoard);
                  }
                  return default;
              })
              .Where(a => a != default)
              .Subscribe(a =>
              {
                  this.Children?.Clear();
                  foreach (var x in a.Item1)
                      this.Children.Add((UIElement)x);

                  a.storyBoard.Begin(this);
              });
        }


    }
}

