using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AnimationPathWpf
{
    public static class StoryBoard
    {
        public static IEnumerable<Timeline> CreateParticleAnimation(Grid runPoint, Geometry particle, double pointTime)
        {
            TransformGroup tfg = new TransformGroup();
            MatrixTransform mtf = new MatrixTransform();
            tfg.Children.Add(mtf);
            TranslateTransform ttf = new TranslateTransform(-runPoint.Width / 2, -runPoint.Height / 2);//纠正最上角沿path运动到中心沿path运动
            tfg.Children.Add(ttf);
            runPoint.RenderTransform = tfg;

            MatrixAnimationUsingPath maup = new MatrixAnimationUsingPath
            {
                PathGeometry = particle.GetFlattenedPathGeometry(),
                Duration = new Duration(TimeSpan.FromSeconds(pointTime)),
                RepeatBehavior = RepeatBehavior.Forever,
                AutoReverse = false,
                IsOffsetCumulative = false,
                DoesRotateWithTangent = true
            };
            Storyboard.SetTarget(maup, runPoint);
            Storyboard.SetTargetProperty(maup, new PropertyPath("(Grid.RenderTransform).Children[0].(MatrixTransform.Matrix)"));
            yield return maup;
        }

        public static IEnumerable<Timeline> CreateTargetAnimation(Ellipse toEll, double pointTime)
        {
            double particleTime = pointTime / 2d;

            var ellda = Animation1(particleTime);
            Storyboard.SetTarget(ellda, toEll);
            Storyboard.SetTargetProperty(ellda, new PropertyPath(Ellipse.OpacityProperty));
            yield return ellda;

            var ca = ColorAnimation(pointTime);
            Storyboard.SetTarget(ca, toEll);
            Storyboard.SetTargetProperty(ca, new PropertyPath("(Ellipse.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Color)"));
            yield return ca;

            RadialGradientBrush rgBrush = new RadialGradientBrush();
            GradientStop gStop0 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop gStopT = new GradientStop(Color.FromArgb(255, 0, 0, 0), 0);
            GradientStop gStop1 = new GradientStop(Color.FromArgb(255, 0, 0, 0), 1);
            rgBrush.GradientStops.Add(gStop0);
            rgBrush.GradientStops.Add(gStopT);
            rgBrush.GradientStops.Add(gStop1);
            toEll.OpacityMask = rgBrush;
        }

        public static LinearGradientBrush GetGradientBrush(Point startPoint, Point endPoint)
        {
            return
               new LinearGradientBrush
               {
                   StartPoint = new Point(startPoint.X > endPoint.X ? 1 : 0, startPoint.Y > endPoint.Y ? 1 : 0),
                   EndPoint = new Point(startPoint.X > endPoint.X ? 0 : 1, startPoint.Y > endPoint.Y ? 0 : 1),
                   GradientStops = new GradientStopCollection(new[]
                   {
                             new GradientStop(Color.FromArgb(255, 0, 0, 0), 0),
                             new GradientStop(Color.FromArgb(0, 0, 0, 0), 0),
                   })
               };
        }

        public static ColorAnimation ColorAnimation(double pointTime) => new ColorAnimation
        {
            To = Color.FromArgb(0, 0, 0, 0),
            Duration = new Duration(TimeSpan.FromSeconds(0)),
            BeginTime = TimeSpan.FromSeconds(pointTime),
            FillBehavior = FillBehavior.HoldEnd
        };

        public static DoubleAnimation Animation1(double particleTime) => new DoubleAnimation
        {
            From = 0.2,
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(particleTime)),
            BeginTime = TimeSpan.FromSeconds(particleTime),
            FillBehavior = FillBehavior.HoldEnd
        };

        public static DoubleAnimation Animation2(double particleTime) => new DoubleAnimation
        {
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(2)),
            RepeatBehavior = RepeatBehavior.Forever,
            BeginTime = TimeSpan.FromSeconds(particleTime)
        };

        public static DoubleAnimation Animation3(double particleTime) => new DoubleAnimation
        {
            To = 1,
            Duration = new Duration(TimeSpan.FromSeconds(particleTime)),
            FillBehavior = FillBehavior.HoldEnd
        };
    }
}