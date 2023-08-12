using UnityEngine;
using Pathfinding;
using UnityEngine.Serialization;

namespace Sources.Modules.Enemy
{
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _maxHealth;
        [FormerlySerializedAs("_aiPath")] [SerializeField] private AIDestinationSetter _destinationSetter;
        
        public EnemyType EnemyType => _enemyType;
        private float _currentHealth;
        
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
                
            }
        }
    }
}
