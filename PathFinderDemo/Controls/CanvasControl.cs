using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DiagramWpf
{
    public class CanvasControl : Canvas, INotifyPropertyChanged
    {
        private Thumb VertexOne, VertexTwo;
        private int vertexSize = 30;

        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(Control), typeof(CanvasControl), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        public CanvasControl()
        {
            VertexOne = new Thumb
            {
                Height = vertexSize,
                Width = vertexSize,
                Background = Brushes.CadetBlue,
                Opacity = 0.3,
            };

            VertexTwo = new Thumb
            {
                Height = vertexSize,
                Width = vertexSize,
                Background = Brushes.GhostWhite,
                Opacity = 0.3,
            };

            Children.Add(VertexOne);
            Children.Add(VertexTwo);

            VertexOne.DragDelta += Vertex_DragDelta;
            VertexTwo.DragDelta += Vertex_DragDelta;

            VertexOne.MouseMove += VertexOne_MouseMove;
            VertexTwo.MouseMove += VertexTwo_MouseMove;

            this.MouseUp += OnMouseUp;
            this.Loaded += MainWindow_Loaded;
        }

        private void Vertex_DragDelta(object sender, DragDeltaEventArgs e)
        {
            UIElement thumb = e.Source as UIElement;
            SetLeft(thumb, GetLeft(thumb) + e.HorizontalChange);
            SetTop(thumb, GetTop(thumb) + e.VerticalChange);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            X1 = Y1 = random.Next(100, 200);
            X2 = Y2 = random.Next(200, 300); ;
            SetTop(VertexOne, Y1 - vertexSize / 2);
            SetLeft(VertexOne, X1 - vertexSize / 2);
            SetTop(VertexTwo, Y2 - vertexSize / 2);
            SetLeft(VertexTwo, X2 - vertexSize / 2);
        }

        #region dependencyproperties

        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set { SetValue(X1Property, value); }
        }

        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set { SetValue(X2Property, value); }
        }

        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set { SetValue(Y1Property, value); }
        }

        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set { SetValue(Y2Property, value); }
        }

        public Control SelectedObject
        {
            get { return (Control)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

        #endregion dependencyproperties

        public bool IsFirstSelected => SelectedObject == VertexOne;
        public bool IsSecondSelected => SelectedObject == VertexTwo;

        private void VertexOne_MouseMove(object sender, MouseEventArgs e)
        {
            VertexMouseMove(sender, e, a => X1 = a, a => Y1 = a);
        }

        private void VertexTwo_MouseMove(object sender, MouseEventArgs e)
        {
            VertexMouseMove(sender, e, a => X2 = a, a => Y2 = a);
        }

        private void VertexMouseMove(object sender, MouseEventArgs e, Action<double> setX, Action<double> setY)
        {
            if (!(sender is Control vertex))
            {
                return;
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFirstSelected)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSecondSelected)));

            var left = GetLeft(vertex);
            var top = GetTop(vertex);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                setY(top + vertexSize / 2);
                setX(left + vertexSize / 2);
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedObject = null;
        }

        private void VertexTwo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }

        private void VertexOne_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
        }
    }
}