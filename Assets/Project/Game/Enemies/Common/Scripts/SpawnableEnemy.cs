using ObjectPooling;
using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(fileName = "Spawnable Enemy", menuName = "Scriptable Objects/Spawnable Enemy")]
    public class SpawnableEnemy : ScriptableObject
    {
        public RecyclableObject recyclableEnemyPrefab;
        public EnemyBaseData enemyBaseData;
    }
}

