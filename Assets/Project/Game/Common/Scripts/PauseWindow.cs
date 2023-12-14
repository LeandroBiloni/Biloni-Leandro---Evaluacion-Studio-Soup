using ObserverPattern;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PauseWindow : MonoBehaviour, IObservable
    {
        private List<IObserver> _observers = new List<IObserver>();

        private void OnDisable()
        {
            NotifyObserver("ClosePause");
        }

        public void Subscribe(IObserver observer)
        {
            if (_observers.Contains(observer) == false)
                _observers.Add(observer);
        }

        public void Unsuscribe(IObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void NotifyObserver(string action)
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].Notify<PauseWindow>(action, this);
            }
        }
    }
}

