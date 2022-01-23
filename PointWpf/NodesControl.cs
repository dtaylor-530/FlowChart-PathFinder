using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace PointWpf
{
    public class NodesControl : ItemsControl
    {
        public object SelectedObject
        {
            get { return (object)GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

        public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register("SelectedObject", typeof(object), typeof(NodesControl), new PropertyMetadata(null));

        static NodesControl()
        {
            NodesControl.ItemsSourceProperty.OverrideMetadata(typeof(NodesControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None, ItemsSourceChanged, ItemsSourceCoerce));
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            foreach (var x in (IEnumerable)e.NewValue)
            {
                ((PointViewModel)x).PropertyChanged -= (a, b) => NodesControl_PropertyChanged(d, x);
                ((PointViewModel)x).PropertyChanged += (a, b) => NodesControl_PropertyChanged(d, x);
            }

            //this.WhenAnyValue(_ => _.PointViewModel).Subscribe(_ =>
            //{
            //    foreach (var p in Points)
            //        if (p != _)
            //            p.IsSelected = false;
            //});
        }

        private static void NodesControl_PropertyChanged(object sender, object e)
        {
            var xx = (NodesControl)sender;
            var pvm = ((PointViewModel)e);
            foreach (var x in xx.Items)
            {
                if (pvm != ((PointViewModel)x))
                    (x as PointViewModel).IsSelected = false;
            }
            xx.Dispatcher.InvokeAsync(() => xx.SelectedObject = pvm, System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));
        }

        private static object ItemsSourceCoerce(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        public NodesControl()
        {
            Uri resourceLocater = new Uri("/PointWpf;component/Themes/Generic.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["One"] as Style;
        }
    }
}