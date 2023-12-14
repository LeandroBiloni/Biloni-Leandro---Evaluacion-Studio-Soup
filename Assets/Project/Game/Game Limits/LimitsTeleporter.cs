using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LimitsTeleporter : MonoBehaviour
    {
        [SerializeField] private Transform _oppositeLimit;
        [SerializeField] private float _offsetForTeleportation;
        private Vector3 _dirToOppositeLimit;

        private void Start()
        {
            _dirToOppositeLimit = (_oppositeLimit.position - transform.position).normalized;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.transform.position = _oppositeLimit.position - (_dirToOppositeLimit * (collision.transform.localScale.x + _offsetForTeleportation)); 
        }
    }
}

