using UnityEngine;

namespace Game.Enemies
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable Objects/Enemy Data")]
    public class EnemyBaseData : ScriptableObject
    {
        public int baseHealthPoints;
        public int baseMovementSpeed;
        [Min(1)] public int minSizeMultiplier;
        public int maxSizeMultiplier;

        [Tooltip("Final score will be decided by Enemy size.")]
        public int baseScore;
    }
}

