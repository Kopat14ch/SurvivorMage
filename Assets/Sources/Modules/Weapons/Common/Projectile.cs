using System.Collections;
using Sources.Modules.Common;
using Sources.Modules.Enemy;
using UnityEngine;

namespace Sources.Modules.Weapons.Common
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField, Range(MinSpeed, MaxSpeed)] private float _speed;

        [SerializeField, Range(MinTimeToDestroy, MaxTimeToDestroy)] private float _timeToDestroy;

        [SerializeField] protected float Damage;

        protected Coroutine DisablingWork;
        protected ShootPoint ShootPoint;
        
        private const int MinSpeed = 1;
        private const int MaxSpeed = 500;
        private const int MinTimeToDestroy = 5;
        private const int MaxTimeToDestroy = 30;

        private Rigidbody2D _rigidbody2D;
        private float _currentTimeToDisable;

        private void Awake() => _rigidbody2D = GetComponent<Rigidbody2D>();

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            bool enemyReceived = other.gameObject.TryGetComponent(out EnemyUnit enemy);
            bool obstacleReceived = other.gameObject.TryGetComponent<Obstacle>(out _);
            
            if (obstacleReceived || enemyReceived)
            {
                gameObject.SetActive(false);
            }
            
            if (enemyReceived)
            {
                enemy.TakeDamage(Damage);
            }
        }
        
        public abstract void Launch(ShootPoint shootPoint, Vector3 position);

        public void Enable() => gameObject.SetActive(true);
        public void Disable() => gameObject.SetActive(false);
        
        protected IEnumerator ChangingPosition(Vector3 position)
        {
            Vector2 direction = (position - ShootPoint.GetPosition()).normalized;

            while (_currentTimeToDisable > 0)
            {
                _rigidbody2D.velocity = direction * _speed;
                
                yield return null;
            }
        }
        
        protected IEnumerator Disabling()
        {
            _currentTimeToDisable = _timeToDestroy;

            while (_currentTimeToDisable > 0)
            {
                _currentTimeToDisable -= Time.deltaTime;

                yield return null;
            }
            
            gameObject.SetActive(false);
        }
    }
}