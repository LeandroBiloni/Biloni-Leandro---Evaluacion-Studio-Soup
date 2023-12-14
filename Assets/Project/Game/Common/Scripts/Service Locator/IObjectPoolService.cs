using ObjectPooling;

namespace ServiceLocating
{
    public interface IObjectPoolService
    {
        ObjectPool GetPool(RecyclableObject recyclableObject);
    }
}

