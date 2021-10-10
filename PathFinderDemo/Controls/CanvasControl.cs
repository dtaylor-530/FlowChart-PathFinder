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

        public CanvasControl()
        {
            VertexOne = new Thumb
            {
                Height = 30,
                Width = 100,
                Background = Brushes.CadetBlue,
            };

            VertexTwo = new Thumb
            {
                Height = 30,
                Width = 100,
                Background = Brushes.AliceBlue,
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
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas.SetTop(VertexOne, Y1 = 300);
            Canvas.SetLeft(VertexOne, X1 = 300);
            Canvas.SetTop(VertexTwo, Y2 = 100);
            Canvas.SetLeft(VertexTwo, X2 = 100);

        }

        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(CanvasControl), new PropertyMetadata());

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(Control), typeof(CanvasControl), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

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

        public bool IsFirstSelected => SelectedObject == VertexOne;
        public bool IsSecondSelected => SelectedObject == VertexTwo;

        private void VertexOne_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedObject = (sender as Control);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFirstSelected)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSecondSelected)));

            var position = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    Y1 = position.Y;
                    X1 = position.X;
                },
                System.Windows.Threading.DispatcherPriority.Background, default);
            }
        }

        private void VertexTwo_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedObject = (sender as Control);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFirstSelected)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSecondSelected)));
            var position = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    Y2 = position.Y;
                    X2 = position.X;
                }, System.Windows.Threading.DispatcherPriority.Background, default);
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
