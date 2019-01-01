using PathFinderLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiagramWpf
{

    public class CustomControl1 : Canvas
    {
        private Label VertexOne;

        private Label VertexTwo;

        public CustomControl1()
        {
            //InitializeComponent();
            Uri resourceLocater = new Uri("/DiagramWpf;component/Themes/Generic.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);

            VertexOne = resourceDictionary["VertexOne"] as Label;
            VertexTwo = resourceDictionary["VertexTwo"] as Label;
            // PathFinderLine = resourceDictionary["PathFinderLine"] as Polyline;

            Children.Add(VertexOne);
            Children.Add(VertexTwo);
            //  Children.Add(PathFinderLine);

            VertexOne.MouseMove += VertexOne_MouseMove;
            VertexTwo.MouseMove += VertexTwo_MouseMove;

            this.MouseUp += OnMouseUp;

            this.Loaded += MainWindow_Loaded;

            this.Height = 500;
            this.Width = 600;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Y1 = (double)VertexOne.GetValue(Canvas.TopProperty);
            X1=(double) VertexOne.GetValue(Canvas.LeftProperty);
            Y2 = (double)VertexTwo.GetValue(Canvas.TopProperty);
            X2 = (double)VertexTwo.GetValue(Canvas.LeftProperty);

        }


        public static readonly DependencyProperty X1Property = DependencyProperty.Register("X1", typeof(double), typeof(CustomControl1), new PropertyMetadata());

        public static readonly DependencyProperty X2Property = DependencyProperty.Register("X2", typeof(double), typeof(CustomControl1), new PropertyMetadata());

        public static readonly DependencyProperty Y1Property = DependencyProperty.Register("Y1", typeof(double), typeof(CustomControl1), new PropertyMetadata());

        public static readonly DependencyProperty Y2Property = DependencyProperty.Register("Y2", typeof(double), typeof(CustomControl1), new PropertyMetadata());

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(Label), typeof(CustomControl1), new PropertyMetadata(null));


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


        public Label SelectedObject
        {
            get { return (Label)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }


        private void VertexOne_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedObject = (sender as Label);
            //vertexOneViewModel.Position = 
            var position = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    X1 = position.X;
                    Y1 = position.Y;
                },
                System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            }
        }

        private void VertexTwo_MouseMove(object sender, MouseEventArgs e)
        {
            SelectedObject = (sender as Label);
            var position = e.GetPosition(this);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    X2 = position.X;
                    Y2 = position.Y;
                },
                System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            }
        }


        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            SelectedObject = null;
        }

        private void VertexTwo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = true;
        }

        private void VertexOne_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = true;
        }




    }
}
