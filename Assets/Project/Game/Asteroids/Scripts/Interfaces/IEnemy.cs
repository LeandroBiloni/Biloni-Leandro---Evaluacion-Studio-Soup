using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemy : IDamageable
    {
        void Initialize();
        void SetData(EnemyData data);
        void SetTarget(Transform target);
        void Death();
    }
}

