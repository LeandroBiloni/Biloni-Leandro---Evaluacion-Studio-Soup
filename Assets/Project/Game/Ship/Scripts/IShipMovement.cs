using UnityEngine;

namespace Game.Ship
{
    public interface IShipMovement
    {
        void Move();
        void Rotate(Vector2 direction);
    }
}

