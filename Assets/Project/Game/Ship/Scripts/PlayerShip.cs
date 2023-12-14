using Game.Audio;
using ObjectPooling;
using ObserverPattern;
using ServiceLocating;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Ship
{

    public class PlayerShip : MonoBehaviour, IShoot, IDamageable, IObservable
    {
        [Header("References")]
        [SerializeField] private RecyclableObject _projectile;

        [SerializeField] private float _movementSpeed;
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private GameObject _shipVisual;
        [SerializeField] private Transform _cannon;
        [SerializeField] private SoundData _deathSound;        

        private BoxCollider2D _collider;
        private Rigidbody2D _rigidBody;
        private InputAction _moveAction;
        private InputAction _rotateAction;
        private InputAction _shootAction;
        private IShipMovement _shipMovement;

        private List<IObserver> _observers = new List<IObserver>();

        private bool _isDead;

        // Start is called before the first frame update
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _shipMovement = new ShipMovement(transform, _movementSpeed, _rigidBody);

            _moveAction = _inputActionAsset.FindAction("Movement");
            _rotateAction = _inputActionAsset.FindAction("Rotation");
            _shootAction = _inputActionAsset.FindAction("Shoot");

            _shootAction.performed += OnShootInputReceived;
        }

        private void Update()
        {
            if (_isDead)
                return;

            CheckMoveInput();
            CheckRotationInput();
        }

        private void CheckMoveInput()
        {
            if (!_moveAction.IsPressed())
                return;
            
            Move();
        }

        private void CheckRotationInput()
        {
            if (!_rotateAction.IsPressed()) 
                return;

            Vector2 dir = _rotateAction.ReadValue<Vector2>();

            Rotate(dir);
        }

        private void Move()
        {
            _shipMovement.Move();
        }

        private void Rotate(Vector2 dir)
        {
            _shipMovement.Rotate(dir);
        }

        private void OnShootInputReceived(InputAction.CallbackContext context)
        {
            if (_isDead)
                return;

            Shoot();
        }

        public void Shoot()
        {
            ObjectPool pool = ServiceLocator.Instance.GetService<IObjectPoolService>().GetPool(_projectile);
            GameObject projectileGameObject = pool.GetGameObject();

            projectileGameObject.transform.position = _cannon.position;

            Projectile projectile = projectileGameObject.GetComponent<Projectile>();
            projectile.Move(transform.up);
        }

        public void TakeDamage(int damage)
        {
            ServiceLocator.Instance.GetService<IAudioService>().GetAudioManager().PlaySound(_deathSound, gameObject);
            NotifyObserver("ShipDeath");
            _shipVisual.SetActive(false);
            _collider.enabled = false;
            _isDead = true;
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
                _observers[i].Notify<PlayerShip>(action, this);
            }
        }
    }
}

