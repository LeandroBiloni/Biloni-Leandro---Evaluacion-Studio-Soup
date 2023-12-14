using ObserverPattern;
using UnityEngine;

namespace Game.Enemies
{
    public interface IEnemy : IDamageable, IObservable
    {
        void Initialize();
        void SetData(EnemyData data);
        void SetTarget(Transform target);
        int GetScore();
        void Death();
    }
}

