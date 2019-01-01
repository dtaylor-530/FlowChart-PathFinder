using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AnimationPathWpf
{
    public static class PathEllipse
    {
        static Random rd = new Random();

        //颜色

        /// <summary>
        /// 添加控件和动画到容器
        /// </summary>
        /// <param name="item">数据项</param>
        public static DependencyObject[] GetAnimation(Point startPoint, Point endPoint, double diameter, PathGeometry geometry, double l, byte[] rgb, Storyboard m_Sb, double m_Speed, string m_PointData)
        {
            //grid_Animation.Children.Clear();
            //m_Sb.Children.Clear();




            // 跑动的点
            System.Windows.Controls.Grid grid = GetRunPoint(rgb, m_PointData);
            //到达城市的圆
            Ellipse ell = PathEllipse.GetToEllipse(diameter, diameter, rgb, endPoint);

            double pointTime = l / m_Speed;

            StoryBoard.AddPointToStoryboard(grid, ell, m_Sb, geometry, l, startPoint, endPoint, pointTime);

            return new DependencyObject[] { grid, ell };
        }


        /// <summary>
        /// 获取跑动的点
        /// </summary>
        /// <param name="rgb">颜色:r,g,b</param>
        /// <returns>Grid</returns>
        public static Grid GetRunPoint(byte[] rgb, string m_PointData)
        {

            //Grid
            Grid grid = new Grid
            {
                IsHitTestVisible = false,//不参与命中测试
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 40,
                Height = 15,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            //Ellipse
            Ellipse ell = new Ellipse
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


            Path path = new Path
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

            grid.Children.Add(ell);
            grid.Children.Add(path);

            return grid;
        }




        /// <summary>
        /// 获取到达城市的圆
        /// </summary>
        /// <param name="toItem">数据项</param>
        /// <param name="rgb">颜色</param>
        /// <returns>Ellipse</returns>
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





        public static Path GetParticlePath(Point start, Point end, byte[] rgb, Storyboard sb, PathGeometry geometry, double l)
        {

            Path path = new Path
            {
                Style = (Style)Application.Current.Resources["ParticlePathStyle"],
                Data = geometry,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(255, rgb[0], rgb[1], rgb[2])),
                OpacityMask = StoryBoard.GetGradientBrush(start, end),

            };

            //path.ToolTip = string.Format("{0}=>{1}",mapitem.ToString(), toItem.To.ToString());

            var particleTime = 1 / l;

            DoubleAnimation pda0 = StoryBoard.Animation3(particleTime);


            //Path particlePath = PathEllipse.GetParticlePath(startPoint, endPoint, rgb, m_angle, out double l);
            //particlePath.

            Storyboard.SetTarget(pda0, path);
            Storyboard.SetTargetProperty(pda0, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[0].(GradientStop.Offset)"));
            sb.Children.Add(pda0);



            var pda1 = StoryBoard.Animation3(particleTime);
            Storyboard.SetTarget(pda1, path);
            Storyboard.SetTargetProperty(pda1, new PropertyPath("(Path.OpacityMask).(GradientBrush.GradientStops)[1].(GradientStop.Offset)"));
            sb.Children.Add(pda1);


            return path;
        }


        /// <summary>
        /// 获取运动轨迹
        /// </summary>
        /// <param name="from">来自</param>
        /// <param name="toItem">去</param>
        /// <param name="rgb">颜色:r,g,b</param>
        /// <param name="l">两点间的直线距离</param>
        /// <returns>Path</returns>
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
