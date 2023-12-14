using ObjectPooling;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _ship;
        [SerializeField] private Vector2 _minDistanceFromCenterToSpawn;
        [SerializeField] private Vector2 _maxDistanceFromCenterToSpawn;
        [SerializeField] private ObjectPoolsManager _poolsManager;
        [SerializeField] private List<SpawnableEnemy> _spawnableEnemies = new List<SpawnableEnemy>();

        private void Start()
        {
            foreach (SpawnableEnemy enemy in _spawnableEnemies)
            {
                _poolsManager.GetPool(enemy.recyclableEnemyPrefab);
            }
            
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
            int random = Random.Range(0, _spawnableEnemies.Count);

            SpawnableEnemy spawnableEnemy = _spawnableEnemies[random];
            RecyclableObject objectToSpawn = spawnableEnemy.recyclableEnemyPrefab;

            GameObject enemyGameObject = _poolsManager.GetPool(objectToSpawn).GetGameObject();

            float x = Random.Range(_minDistanceFromCenterToSpawn.x, _maxDistanceFromCenterToSpawn.x);
            float y = Random.Range(_minDistanceFromCenterToSpawn.y, _maxDistanceFromCenterToSpawn.y);
            enemyGameObject.transform.position = new Vector3(x, y);
            
            IEnemy enemy = enemyGameObject.GetComponent<IEnemy>();

            EnemyBaseData baseData = spawnableEnemy.enemyBaseData;

            int sizeMultiplier = Random.Range(baseData.minSizeMultiplier, baseData.maxSizeMultiplier);
            enemyGameObject.transform.localScale *= sizeMultiplier;

            int healthPoints = baseData.baseHealthPoints * sizeMultiplier;
            float moveSpeed = baseData.baseMovementSpeed * sizeMultiplier;
            int score = baseData.baseScore * sizeMultiplier;

            EnemyData enemyData = new EnemyData(healthPoints, moveSpeed, score);
            enemy.SetData(enemyData);

            enemy.SetTarget(_ship);

            enemy.Initialize();
        }
    }
}

