using UnityEngine;

namespace Sources.Modules.Player
{
    public class Mage : MonoBehaviour
    {
        private const float MaxHealth = 100;

        private float _currentHealth;

        private void Awake() => _currentHealth = MaxHealth;

        public void TryTakeDamage(float damage)
        {
            if (damage > 0 && _currentHealth > 0)
            {
                _currentHealth -= damage;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, MaxHealth);

                if (_currentHealth <= 0)
                {
                    Die();
                }
            }
        }

        private void Die()
        {
            Debug.Log("die");
        }
    }
}