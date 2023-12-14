using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemy : IDamageable
    {
        void SetTarget(Transform target);
        void Death();
    }
}

