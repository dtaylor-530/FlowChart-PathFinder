using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using PathFinderLibrary;

namespace PathFinderDemo
{
    //public enum Option { Option1, Option2, Option3 }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private PathLine pathLine = PathLine.Orthogonal;

        public MainWindow()
        {
            InitializeComponent();

            //VertexOne.MouseMove += VertexOne_MouseMove;
            //VertexTwo.MouseMove += VertexTwo_MouseMove;

            //MainCanvas.MouseUp += OnMouseUp;
            //vertexOneViewModel = new PathFinderLibrary.ConnectionPoint("left");
            //vertexTwoViewModel = new ConnectionPoint("right");
            ////PathCalculator.PathLine = PathLine.Orthogonal;
            //this.Loaded += MainWindow_Loaded;
            //this.DataContext = this;
            //this.PropertyChanged += MainWindow_PropertyChanged;
        }

      //  private void MainWindow_PropertyChanged(object sender, PropertyChangedEventArgs e)
      //  {
      //      if (e.PropertyName == nameof(PathLine))
      //      {
      //          UpdateConnectionPoints0();
      //      }
      //  }

      //  private void MainWindow_Loaded(object sender, RoutedEventArgs e)
      //  {
      //      vertexOneViewModel.Position = VertexOne.TransformToAncestor(MainCanvas)
      //.Transform(new Point(0, 0));

      //      vertexTwoViewModel.Position = VertexTwo.TransformToAncestor(MainCanvas)
      //        .Transform(new Point(0, 0));
      //      UpdateConnectionPoints0();
      //  }



      //  public PathLine PathLine
      //  {
      //      get { return pathLine; }
      //      set { pathLine = value; OnPropertyChanged(nameof(PathLine)); }

      //  }

      //  private void VertexOne_MouseMove(object sender, MouseEventArgs e)
      //  {
      //      if (e.LeftButton == MouseButtonState.Pressed)
      //      {
      //          vertexOneViewModel.Position = e.GetPosition(this);
      //          UpdateConnectionPoints0();
      //      }
      //  }

      //  private void VertexTwo_MouseMove(object sender, MouseEventArgs e)
      //  {
      //      if (e.LeftButton == MouseButtonState.Pressed)
      //      {
      //          vertexTwoViewModel.Position = e.GetPosition(this);
      //          UpdateConnectionPoints0();
      //      }
      //  }

      //  private ConnectionPoint vertexOneViewModel
      //  {
      //      get; set;
      //  }

      //  private ConnectionPoint vertexTwoViewModel
      //  {
      //      get; set;
      //  }

      //  //public PathCalculator pathFinder
      //  //{
      //  //    get; set;
      //  //}

      //  private void OnMouseUp(object sender, MouseButtonEventArgs e)
      //  {
      //      Vertex = null;


      //  }


      //  private Label Vertex;

      //  public event PropertyChangedEventHandler PropertyChanged;

      //  public void OnPropertyChanged(string name)
      //  {
      //      if (PropertyChanged != null)
      //          PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
      //  }

      //  private void VertexTwo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      //  {

      //      base.OnMouseLeftButtonDown(e);
      //      Vertex = VertexTwo;
      //      // e.Handled = true;
      //  }

      //  private void VertexOne_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      //  {
      //      base.OnMouseLeftButtonDown(e);
      //      Vertex = VertexOne;
      //      // e.Handled = true;
      //  }



      //  private void UpdateConnectionPoints0()
      //  {

      //      List<Point> points = PathCalculator.FindPath(vertexOneViewModel, vertexTwoViewModel, PathLine);
      //      PointCollection pointCollection = new PointCollection();
      //      foreach (Point point in points)
      //      {
      //          pointCollection.Add(point);
      //      }
      //      PathFinderLine.Points = pointCollection;

      //  }





    }

}

