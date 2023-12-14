using ObjectPooling;
using System.Collections;
using UnityEngine;

namespace Game.Enemies
{
    public class Asteroid : RecyclableObject, IEnemy
    {
        private int _currentHealthPoints;
        private EnemyData _data;
        private Transform _transform;
        private Vector3 _movementDir;

        private void Update()
        {
            _transform.position += _movementDir * (_data.MovementSpeed * Time.deltaTime);
        }

        public void Initialize()
        {
            _currentHealthPoints = _data.MaxHealthPoints;

            gameObject.SetActive(true);

            StartCoroutine(AutoDestroy());
        }

        public void TakeDamage(int damage)
        {
            int newHealthValue = _currentHealthPoints - damage;
            _currentHealthPoints = Mathf.Clamp(newHealthValue, 0, _data.MaxHealthPoints);

            if (_currentHealthPoints <= 0)
                Death();
        }

        public void Death()
        {
            Recycle();
        }

        public override void OnRevived()
        {
            _transform = transform;
            _transform.localScale = Vector3.one;
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

            Death();
        }

        public void SetTarget(Transform target)
        {
            _movementDir = (target.position - transform.position).normalized;

            Debug.DrawRay(_transform.position, target.position - transform.position, Color.red, 5);
        }

        public void SetData(EnemyData data)
        {
            _data = data;  
        }
    }
}

