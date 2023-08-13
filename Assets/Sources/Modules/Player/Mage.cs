using Sources.Modules.Player.Animation;
using UnityEngine;

namespace Sources.Modules.Player
{
    [RequireComponent(typeof(Animator))]
    public class Mage : MonoBehaviour
    {
        private Animator _animator;
        
        private const float MaxHealth = 100;

        private float _currentHealth;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = MaxHealth;
        }


        public void TryTakeDamage(float damage)
        {
            if (damage > 0 && _currentHealth > 0)
            {
                _animator.Play(PlayerAnimator.States.Hit);
                
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