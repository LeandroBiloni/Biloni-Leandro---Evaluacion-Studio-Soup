using Game.Enemies;
using ObjectPooling;
using ServiceLocating;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Ship
{

    public class PlayerShip : MonoBehaviour, IShoot, IDamageable
    {
        [Header("References")]
        [SerializeField] private RecyclableObject _projectile;

        [SerializeField] private float _movementSpeed;
        [SerializeField] private InputActionAsset _inputActionAsset;
        [SerializeField] private Transform _cannon;

        private Rigidbody2D _rigidBody;
        private InputAction _moveAction;
        private InputAction _rotateAction;
        private InputAction _shootAction;
        private IShipMovement _shipMovement;

        // Start is called before the first frame update
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();

            _shipMovement = new ShipMovement(transform, _movementSpeed, _rigidBody);

            _moveAction = _inputActionAsset.FindAction("Movement");
            _rotateAction = _inputActionAsset.FindAction("Rotation");
            _shootAction = _inputActionAsset.FindAction("Shoot");

            _shootAction.performed += OnShootInputReceived;
        }

        private void Update()
        {
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
            Debug.Log("Ship destroyed!");
            gameObject.SetActive(false);
        }
    }
}

