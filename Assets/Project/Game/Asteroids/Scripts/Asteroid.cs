using ObjectPooling;
using System.Collections;
using UnityEngine;

namespace Game.Enemies
{

    public class Asteroid : RecyclableObject, IEnemy
    {
        [Header("References")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _maxHealthPoints;

        private int _currentHealthPoints;

        [SerializeField] private Vector3 _movementDir;
        private Transform _transform;

        private void Update()
        {
            _transform.position += _movementDir * (_movementSpeed * Time.deltaTime);
        }

        private void Initialize()
        {
            _transform = transform;
            _currentHealthPoints = _maxHealthPoints;
            gameObject.SetActive(true);
        }

        public void TakeDamage(int damage)
        {
            int newHealthValue = _currentHealthPoints - damage;
            _currentHealthPoints = Mathf.Clamp(newHealthValue, 0, _maxHealthPoints);

            if (_currentHealthPoints <= 0)
                Death();
        }

        public void Death()
        {
            Destroy(gameObject);
        }

        public override void OnRevived()
        {
            Initialize();

            StartCoroutine(AutoDestroy());
        }

        public override void OnRecycle()
        {
            Debug.Log("Recycle asteroid!");

            gameObject.SetActive(false);
        }

        private IEnumerator AutoDestroy()
        {
            float duration = 3;

            while (duration > 0)
            {
                duration -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            Recycle();
        }

        public void SetTarget(Transform target)
        {
            _movementDir = (target.position - transform.position).normalized;

            Debug.DrawRay(_transform.position, target.position - transform.position, Color.red, 5);
        }
    }
}

