using PointWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PathFinderDemo
{
    public class DesignData
    {

        private readonly Lazy<ObservableCollection<PointViewModel>> _someField = new Lazy<ObservableCollection<PointViewModel>>(() => new ObservableCollection<PointViewModel>(
            new[]{
            new PointViewModel { X = 50, Y = 70 },
                        new PointViewModel { X = 120, Y = 130 },
                        new PointViewModel { X = 70, Y = 102 }
            }));


        public ObservableCollection<PointViewModel> Points
        {
            get
            {
                return _someField.Value;
            }
        }

    }
}
