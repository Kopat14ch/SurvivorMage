using UnityEngine;

namespace Sources.Modules.Enemy
{
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        private float _currentHealth;

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
