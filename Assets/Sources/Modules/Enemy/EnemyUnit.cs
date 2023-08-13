using System;
using UnityEngine;
using Pathfinding;

namespace Sources.Modules.Enemy
{
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _maxHealth;
        [SerializeField] private AIDestinationSetter _destinationSetter;
        
        public EnemyType EnemyType => _enemyType;

        public event Action<EnemyUnit> Died;
        private float _currentHealth;

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

        private void TryDie()
        {
            if (_currentHealth == 0)
            {
                Died?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
