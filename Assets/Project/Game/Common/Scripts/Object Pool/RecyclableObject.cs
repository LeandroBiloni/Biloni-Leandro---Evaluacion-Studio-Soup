using UnityEngine;

namespace ObjectPooling
{
    public abstract class RecyclableObject : MonoBehaviour
    {
        private IObjectPool _objectPool;

        public void SetObjectPool(IObjectPool objectPool)
        {
            _objectPool = objectPool;
        }

        public abstract void OnRevived();

        public abstract void OnRecycle();

        /// <summary>
        /// Returns the object to the pool
        /// </summary>
        public void Recycle()
        {
            _objectPool.RecycleObject(this);
        }
    }
}
