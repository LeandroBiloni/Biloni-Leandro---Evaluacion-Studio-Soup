using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Ship
{
    public class Ship : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private InputActionAsset _inputActionAsset;

        private Rigidbody2D _rigidBody;
        private InputAction _moveAction;
        private InputAction _rotateAction;

        private IShipMovement _shipMovement;

        // Start is called before the first frame update
        void Start()
        {
            _rigidBody = GetComponent<Rigidbody2D>();

            _shipMovement = new ShipMovement(transform, _movementSpeed, _rigidBody);

            _moveAction = _inputActionAsset.FindAction("Movement");
            _rotateAction = _inputActionAsset.FindAction("Rotation");
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
    }
}

