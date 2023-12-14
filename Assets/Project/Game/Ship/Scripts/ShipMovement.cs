using UnityEngine;

namespace Game.Ship
{
    public class ShipMovement : IShipMovement
    {
        private readonly Transform _shipTransform;
        private readonly Rigidbody2D _shipRigidbody;
        private readonly float _movementSpeed;

        public ShipMovement(Transform shipTransform, float movementSpeed, Rigidbody2D shipRigidbody)
        {
            _shipTransform = shipTransform;
            _movementSpeed = movementSpeed;
            _shipRigidbody = shipRigidbody;
        }

        public void Move()
        {
            Vector2 forceDirection = _shipTransform.up * _movementSpeed;
            _shipRigidbody.AddForce(forceDirection);
        }

        public void Rotate(Vector2 direction)
        {
            Vector3 fixedDir = new Vector3(0, 0, direction.x);

            _shipTransform.Rotate(fixedDir);
        }
    }
}

