using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocating
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _serviceLocator;

        public static ServiceLocator Instance => _serviceLocator;

        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        private void Awake()
        {
            if (_serviceLocator == null)
            {
                _serviceLocator = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RegisterService<T>(T service)
        {
            if (!_services.ContainsKey(typeof(T)))
                _services.Add(typeof(T), service);
        }

        public T GetService<T>()
        {
            return (T)_services[typeof(T)];
        }
    }
}

