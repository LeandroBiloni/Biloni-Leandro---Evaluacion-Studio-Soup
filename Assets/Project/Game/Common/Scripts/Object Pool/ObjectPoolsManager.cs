using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPoolsManager : MonoBehaviour
    {
        private Dictionary<RecyclableObject, ObjectPool> _objectPools = new Dictionary<RecyclableObject, ObjectPool> ();

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