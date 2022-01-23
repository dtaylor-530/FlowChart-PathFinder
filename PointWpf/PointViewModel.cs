using inpce.core.Library.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace PointWpf
{
    public class PointViewModel : INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private bool _isSelected;

        public int X
        {
            get { return _x; }
            set { this.SetProperty(ref _x, value, PropertyChanged); }
        }

        public int Y
        {
            get { return _y; }
            set { this.SetProperty(ref _y, value, PropertyChanged); }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { this.SetProperty(ref _isSelected, value, PropertyChanged); }
        }

        public PointViewModel()
        {
            Command = new Command(this);
            DragCommand = new DragCommand(this);
        }

        [Browsable(false)]
        public ICommand Command { get; }

        [Browsable(false)]
        public ICommand DragCommand { get; }

        [Browsable(false)]
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Command : ICommand
    {
        private PointViewModel pvm;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public Command(PointViewModel pvm)
        {
            this.pvm = pvm;
        }

        public void Execute(object parameter)
        {
            (pvm as PointViewModel).IsSelected = true;
        }
    }

    public class DragCommand : ICommand
    {
        private PointViewModel pvm;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public DragCommand(PointViewModel pvm)
        {
            this.pvm = pvm;
        }

        public void Execute(object parameter)
        {
            (pvm as PointViewModel).X += (int)(parameter as DragDeltaEventArgs).HorizontalChange;
            (pvm as PointViewModel).Y += (int)(parameter as DragDeltaEventArgs).VerticalChange;
            (parameter as DragDeltaEventArgs).Handled = true;
        }
    }
}