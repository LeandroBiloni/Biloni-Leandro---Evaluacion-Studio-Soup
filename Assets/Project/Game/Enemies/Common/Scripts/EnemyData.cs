namespace Game.Enemies
{
    public class EnemyData
    {
        private readonly int _maxHealthPoints;
        private readonly float _movementSpeed;
        private readonly int _score;

        public int MaxHealthPoints => _maxHealthPoints;
        public float MovementSpeed => _movementSpeed;
        public int Score => _score;

        public EnemyData(int maxHealthPoints, float movementSpeed, int score)
        {
            _maxHealthPoints = maxHealthPoints;
            _movementSpeed = movementSpeed;
            _score = score;
        }
    }
}

