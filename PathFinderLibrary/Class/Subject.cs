using System;
using System.Collections.Generic;

namespace PathFinderLibrary
{
    public class Observer<T> : IObserver<T>
    {
        private IDisposable unsubscriber;

        public virtual void Subscribe(IObservable<T> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("The Location Tracker has completed transmitting data .");
            this.Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {
            Console.WriteLine("The location cannot be determined.");
        }

        public virtual void OnNext(T value)
        {
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }

    public class Subject<T> : Observer<T>, IObservable<T>
    {
        private List<IObserver<T>> observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber<T>(observers, observer);
        }

        public override void OnNext(T value)
        {
            observers.ForEach(_ => _.OnNext(value));
        }
    }

    internal class Unsubscriber<T> : IDisposable
    {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}