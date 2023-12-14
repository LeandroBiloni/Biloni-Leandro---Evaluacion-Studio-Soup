using ServiceLocating;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPoolsManager : MonoBehaviour, IObjectPoolService
    {
        private Dictionary<RecyclableObject, ObjectPool> _objectPools = new Dictionary<RecyclableObject, ObjectPool> ();

        private void Start()
        {
            ServiceLocator.Instance.RegisterService<IObjectPoolService>(this);
        }

        public ObjectPool GetPool(RecyclableObject recyclableObject)
        {
            ObjectPool pool;

            if (!_objectPools.TryGetValue(recyclableObject, out pool))
            {
                pool = new ObjectPool(recyclableObject);
                _objectPools.Add(recyclableObject, pool);
            }

            return pool;
        }
    }
}