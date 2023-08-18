using System;
using UnityEngine;
using Pathfinding;
using Sources.Modules.Particles.Scripts;

namespace Sources.Modules.Enemy
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _maxHealth;
        [SerializeField] private AIDestinationSetter _destinationSetter;
        [SerializeField] private ParticleType _diedType;

        private float _currentHealth;
        private ParticleSpawner _particleSpawner;
        
        public event Action<EnemyUnit> Died;

        public CapsuleCollider2D Collider2D { get; private set; }
        public bool IsDie => _currentHealth <= 0;
        public EnemyType EnemyType => _enemyType;

        private void Awake() => Collider2D = GetComponent<CapsuleCollider2D>();

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        public void SetParticleSpawner(ParticleSpawner particleSpawner)
        {
            _particleSpawner = particleSpawner;
            gameObject.GetComponent<EnemyAttack>().SetParticleSpawner(particleSpawner);
        }
        
        public void SetTarget(Transform target)
        {
            _destinationSetter.target = target;
        }
        
        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            
            TryDie();
        }

        private void TryDie()
        {
            if (_currentHealth <= 0)
            {
                _particleSpawner.SpawnParticle(_diedType, transform.position);
                Died?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
