using System;
using UnityEngine;
using Pathfinding;

namespace Sources.Modules.Enemy
{
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _maxHealth;
        [SerializeField] private AIDestinationSetter _destinationSetter;

        private const float AddMaxHealth = 35;

        private EnemyAttack _attack;
        private float _currentHealth;

        public event Action<EnemyUnit> Died;

        public CapsuleCollider2D Collider2D { get; private set; }
        
        public int CurrentLevel { get; private set; }
        public bool IsDie => _currentHealth <= 0;
        public EnemyType EnemyType => _enemyType;

        private void Awake()
        {
            Collider2D = GetComponent<CapsuleCollider2D>();
            _attack = GetComponent<EnemyAttack>();
            CurrentLevel = 1;
        }

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
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

        public void AddLevels(int wave)
        {
            _attack.AddLevel(wave, CurrentLevel);
            
            while (CurrentLevel < wave)
            {
                _maxHealth += AddMaxHealth;
                CurrentLevel++;
            }
        }

        private void TryDie()
        {
            if (_currentHealth <= 0)
            {
                Died?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
