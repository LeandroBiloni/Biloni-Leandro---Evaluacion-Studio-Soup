using Game.Audio;
using ObjectPooling;
using ServiceLocating;
using UnityEngine;

namespace Game.Ship
{
    public class Projectile : RecyclableObject, IProjectileMovement
    {
        [Header("References")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private SoundData _shootSound;

        private Vector3 _movementDir;

        private bool _canMove;

        private void Update()
        {
            if (!_canMove)
                return;

            transform.position += _movementDir * (_movementSpeed * Time.deltaTime);
        }
        public void Move(Vector3 dir)
        {
            _movementDir = dir;
            _canMove = true;
        }

        public override void OnRevived()
        {
            _canMove = false;
            _movementDir = Vector3.zero;
            gameObject.SetActive(true);

            ServiceLocator.Instance.GetService<IAudioService>().GetAudioManager().PlaySound(_shootSound, gameObject);
        }

        public override void OnRecycle()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
                damageable.TakeDamage(_damage);

            Death();
            
        }

        private void Death()
        {
            Recycle();
        }
    }
}

