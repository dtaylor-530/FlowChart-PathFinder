using System.Windows;

namespace PathFinder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            //// Setup Quick Converter.
            //// Add the System namespace so we can use primitive types (i.e. int, etc.).
            //QuickConverter.EquationTokenizer.AddNamespace(typeof(object));
            //// Add the System.Windows namespace so we can use Visibility.Collapsed, etc.
            //QuickConverter.EquationTokenizer.AddNamespace(typeof(System.Windows.Visibility));
        }
    }
}