using UnityEngine;


namespace ObjectPooling
{
    public interface IObjectPool
    {
        void RecycleAllObjecstInUse();
        GameObject GetGameObject();
        void RecycleObject(RecyclableObject recyclableObject);
        int GetPoolSize();
    }
}
