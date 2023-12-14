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

        private RecyclableObject ReturnObjectInUse()
        {
            RecyclableObject recyclableObject = _objectsInUse[0];

            RecycleObject(recyclableObject);

            return recyclableObject;
        }

        public void RecycleObject(RecyclableObject recyclableObject)
        {
            recyclableObject.OnRecycle();

            _objectsInUse.Remove(recyclableObject);

            _objectsPool.Enqueue(recyclableObject);
        }

        public void RecycleAllObjecstInUse()
        {
            for (var i = _objectsInUse.Count - 1; i >= 0; i--)
            {
                RecycleObject(_objectsInUse[i]);
            }
        }

        public int GetPoolSize()
        {
            return _objectsInPoolCount;
        }
    }
}