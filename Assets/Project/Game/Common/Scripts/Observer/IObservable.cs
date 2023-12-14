using System;

namespace ObserverPattern
{
    public interface IObservable
    {
        void Subscribe(IObserver observer);
        void Unsuscribe(IObserver observer);
        void NotifyObserver(string action);
    }
}
