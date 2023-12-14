using Game.Audio;
using Game.Ship;
using ObjectPooling;
using ObserverPattern;
using ServiceLocating;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemies
{
    public class Asteroid : RecyclableObject, IEnemy
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private CircleCollider2D _circleCollider;
        [SerializeField] private SoundData _deathSound;

        private int _currentHealthPoints;
        private EnemyData _data;
        private Transform _transform;
        private Vector3 _movementDir;

        private List<IObserver> _observers = new List<IObserver>();

        private void Update()
        {
            _transform.position += _movementDir * (_data.MovementSpeed * Time.deltaTime);
        }

        public void Initialize()
        {
            _currentHealthPoints = _data.MaxHealthPoints;

            gameObject.SetActive(true);
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
            StartCoroutine(DeathDelay());
        }

        private IEnumerator DeathDelay()
        {
            ServiceLocator.Instance.GetService<IAudioService>().GetAudioManager().PlaySound(_deathSound, gameObject);
            _circleCollider.enabled = false;
            _visual.SetActive(false);
            NotifyObserver("AsteroidDeath");

            yield return new WaitForSeconds(_deathSound.clip.length);

            Recycle();
        }

        public override void OnRevived()
        {
            _transform = transform;
            _transform.localScale = Vector3.one;
        }

        public override void OnRecycle()
        {
            gameObject.SetActive(false);
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

        public int GetScore()
        {
            return _data.Score;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent<PlayerShip>(out PlayerShip ship))
                return;

            ship.TakeDamage(0);
        }

        public void Subscribe(IObserver observer)
        {
            if (_observers.Contains(observer) == false)
                _observers.Add(observer);
        }

        public void Unsuscribe(IObserver observer)
        {
            if (_observers.Contains(observer))
                _observers.Remove(observer);
        }

        public void NotifyObserver(string action)
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].Notify<Asteroid>(action, this);
            }
        }
    }
}

