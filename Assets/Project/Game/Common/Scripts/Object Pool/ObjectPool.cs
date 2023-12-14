using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [System.Serializable]
    public class ObjectPool : IObjectPool
    {
        private readonly RecyclableObject _recyclableObjectPrefab;

        private readonly int _maxInstanciedObjects;

        private readonly Queue<RecyclableObject> _objectsPool;

        private readonly List<RecyclableObject> _objectsInUse;

        private int _objectsInPoolCount;

        public ObjectPool(RecyclableObject recyclableObjectPrefab, int maxInstanciedObjects = 1000)
        {
            _recyclableObjectPrefab = recyclableObjectPrefab;
            _maxInstanciedObjects = maxInstanciedObjects;

            _objectsPool = new Queue<RecyclableObject>();
            _objectsInUse = new List<RecyclableObject>();
        }

        /// <summary>
        ///  Return a instancied gameobject or if there aren't instacied gameobjects, then instantiate a new one
        /// </summary>
        /// <returns></returns>
        public GameObject GetGameObject()
        {
            RecyclableObject recyclablObject;
            recyclablObject = GetRecycleObject();

            _objectsInUse.Add(recyclablObject);

            recyclablObject.OnRevived();

            return recyclablObject.gameObject;
        }

        private RecyclableObject GetRecycleObject()
        {
            RecyclableObject recyclablObject;

            if (ThereAreObjectInThePool())
                recyclablObject = _objectsPool.Dequeue();

            else if (IsThePoolFull())
                recyclablObject = ReturnObjectInUse();

            else
                recyclablObject = GenerateNewObject();

            return recyclablObject;
        }

        private bool ThereAreObjectInThePool()
        {
            return _objectsPool.Count > 0;
        }

        private bool IsThePoolFull()
        {
            return _objectsInPoolCount >= _maxInstanciedObjects;
        }

        private RecyclableObject GenerateNewObject()
        {
            RecyclableObject recyclableObject = Object.Instantiate(_recyclableObjectPrefab);

            recyclableObject.SetObjectPool(this);

            _objectsInPoolCount++;

            return recyclableObject;
        }

        /// <summary>
        /// Return the first RecyclableObject in the InUse List
        /// </summary>
        /// <returns></returns>
        private RecyclableObject ReturnObjectInUse()
        {
            RecyclableObject recyclableObject = _objectsInUse[0];

            RecycleObject(recyclableObject);

            return recyclableObject;
        }

        /// <summary>
        /// Recycle a object that is in use
        /// </summary>
        /// <param name="recyclableObject"></param>
        public void RecycleObject(RecyclableObject recyclableObject)
        {
            recyclableObject.OnRecycle();

            _objectsInUse.Remove(recyclableObject);

            _objectsPool.Enqueue(recyclableObject);
        }

        /// <summary>
        /// Recycle all objects
        /// </summary>
        public void RecycleAllObjecstInUse()
        {
            for (var i = _objectsInUse.Count - 1; i >= 0; i--)
            {
                RecycleObject(_objectsInUse[i]);
            }
        }

        /// <summary>
        /// Get the total instancied objects
        /// </summary>
        /// <returns></returns>
        public int GetPoolSize()
        {
            return _objectsInPoolCount;
        }
    }
}