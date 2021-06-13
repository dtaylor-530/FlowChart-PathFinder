using PointWpf;
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

namespace PathFinderDemo
{
    /// <summary>
    /// Interaction logic for UserControl2.xaml
    /// </summary>
    public partial class UserControl2 : UserControl
    {
        public UserControl2()
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
