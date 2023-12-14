using ObjectPooling;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _ship;
        [SerializeField] private ObjectPoolsManager _poolsManager;
        [SerializeField] private Asteroid _asteroidPrefab;

        private ObjectPool _pool;

        private void Start()
        {
            _pool = _poolsManager.GetPool(_asteroidPrefab);
        }

        [SerializeField] private float _timeToSpawn = 2;

        private float _currentTime = 0;

        private void Update()
        {
            if (_currentTime < _timeToSpawn)
                _currentTime += Time.deltaTime;
            else
            {
                _currentTime = 0;
                Spawn();
            }
        }

        void Spawn()
        {
            GameObject enemyGameObject = _pool.GetGameObject();

            float x = Random.Range(-10, 10);
            float y = Random.Range(-10, 10);
            enemyGameObject.transform.position = new Vector3(x, y);
            
            IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();
            enemy.SetTarget(_ship);
        }
    }
}

