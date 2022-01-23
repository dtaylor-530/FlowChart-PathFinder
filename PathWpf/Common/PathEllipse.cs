using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AnimationPathWpf
{
    public static class PathEllipse
    {
        public static IEnumerable<DependencyObject> GetAnimations(
            Point startPoint,
            Point endPoint,
            double diameter,
            Geometry geometry,
            byte[] rgb,
            Storyboard m_Sb,
            double pointTime,
            string m_PointData,
            double l,
            Style style,
            bool showPathAnimation,
            bool showParticleAnimation,
            bool showTargetAnimation)
        {
            if (showPathAnimation)
            {
                Path path = new Path
                {
                    Style = style,
                    Data = geometry,
                    Stroke = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
                    OpacityMask = StoryBoard.GetGradientBrush(startPoint, endPoint),
                };

                foreach (var timeLine in PathEllipse.GetPathAnimation(path, startPoint, endPoint, rgb, m_Sb, geometry, l, style))
                {
                    m_Sb.Children.Add(timeLine);
                }
                yield return path;
            }
            if (showParticleAnimation)
            {
                Grid particle = GetRunPoint(rgb, m_PointData);

                foreach (var timeLine in StoryBoard.CreateParticleAnimation(particle, geometry, pointTime))
                {
                    m_Sb.Children.Add(timeLine);
                }
                yield return particle;
            }
            if (showTargetAnimation)
            {
                Ellipse ell = PathEllipse.GetToEllipse(diameter, diameter, rgb, endPoint);
                foreach (var timeLine in StoryBoard.CreateTargetAnimation(ell, pointTime))
                {
                    m_Sb.Children.Add(timeLine);
                }
                yield return ell;
            }
        }

        public static Grid GetRunPoint(byte[] rgb, string m_PointData)
        {
            Grid grid = new Grid
            {
                IsHitTestVisible = false,//不参与命中测试
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 40,
                Height = 15,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            Ellipse ell = GetEllipse(rgb);

            Path path = GetPath(rgb, m_PointData);
            grid.Children.Add(ell);
            grid.Children.Add(path);

            return grid;
        }

        private static Ellipse GetEllipse(byte[] rgb)
        {
            return new Ellipse
            {
                Width = 40,
                Height = 15,
                Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
                OpacityMask = new RadialGradientBrush
                {
                    GradientOrigin = new Point(0.8, 0.5),
                    GradientStops = new GradientStopCollection(new[]
                {
                    new GradientStop(Color.FromArgb(255, 0, 0, 0), 0),
                    new GradientStop(Color.FromArgb(22, 0, 0, 0), 1)
                })
                }
            };
        }

        private static Path GetPath(byte[] rgb, string m_PointData)
        {
            return new Path
            {
                Data = Geometry.Parse(m_PointData),
                Width = 30,
                Height = 4,
                Fill = new LinearGradientBrush
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new GradientStopCollection(
                        new[]{
                            new GradientStop(Color.FromArgb(88, rgb[0], rgb[1], rgb[2]), 0),
                            new GradientStop(Color.FromArgb(255, 255, 255, 255), 1)
                        })
                },
                Stretch = Stretch.Fill
            };
        }

        public static Ellipse GetToEllipse(double width, double height, byte[] rgb, Point toPos) => new Ellipse
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Width = width,
            Height = height,
            Fill = new SolidColorBrush(Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
            RenderTransform = new TranslateTransform(toPos.X - width / 2, toPos.Y - height / 2),
            Opacity = 0,
        };

        public static IEnumerable<Timeline> GetPathAnimation(Path path, Point start, Point end, byte[] rgb, Storyboard sb, Geometry geometry, double l, Style style)
        {
            //path.ToolTip = string.Format("{0}=>{1}",mapitem.ToString(), toItem.To.ToString());

            var particleTime = 1 / l;

            DoubleAnimation pda0 = StoryBoard.Animation3(particleTime);

            Storyboard.SetTarget(pda0, path);
            Storyboard.SetTargetProperty(pda0, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[0].(GradientStop.Offset)"));
            yield return (pda0);

            var pda1 = StoryBoard.Animation3(particleTime);
            Storyboard.SetTarget(pda1, path);
            Storyboard.SetTargetProperty(pda1, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
            yield return (pda1);
        }

        public static PathGeometry GetParticlePathGeometry(Point start, Point end, double m_Angle)
        {
            PathGeometry pg = new PathGeometry();
            PathFigure pf = new PathFigure();
            pf.StartPoint = start;
            ArcSegment arc = new ArcSegment();
            arc.SweepDirection = SweepDirection.Clockwise;//顺时针弧
            arc.Point = end;

            double sinA = Math.Sin(Math.PI * m_Angle / 180.0);

            double x = start.X - end.X;
            double y = start.Y - end.Y;
            double aa = x * x + y * y;
            double l = Math.Sqrt(aa);
            double r = l / (sinA * 2);
            arc.Size = new Size(r, r);
            pf.Segments.Add(arc);
            pg.Figures.Add(pf);

            return pg;
        }
    }
}