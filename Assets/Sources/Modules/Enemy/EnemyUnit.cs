using UnityEngine;

namespace Sources.Modules.Enemy
{
    public class EnemyUnit : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        public void TakeDamage(int damage)
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
