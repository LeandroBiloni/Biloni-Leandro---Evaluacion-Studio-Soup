using Game;
using ServiceLocating;
using System.Collections.Generic;

namespace ObjectPooling
{
    public class ObjectPoolsManager : Installer, IObjectPoolService
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

        public override void Install()
        {
            ServiceLocator.Instance.RegisterService<IObjectPoolService>(this);
        }
    }
}