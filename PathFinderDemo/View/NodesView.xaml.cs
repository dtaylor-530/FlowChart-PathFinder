using PointWpf;
using System.Windows.Controls;

namespace PathFinderDemo
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class NodesView : UserControl
    {
        public NodesView()
        {
            InitializeComponent();
        }
    }

    public class DesignData
    {
        public PointViewModel[] Points => new[] {
            new PointViewModel { X = 50, Y = 70 },
            new PointViewModel { X = 120, Y = 130 },
            new PointViewModel { X = 70, Y = 102 }
            };
    }
}