using System;
using Sources.Modules.Player.Animation;
using UnityEngine;

namespace Sources.Modules.Player
{
    [RequireComponent(typeof(Animator))]
    public class Mage : MonoBehaviour
    {
        private Animator _animator;
        private float _maxHealth = 300;
        private float _currentHealth;

        public event Action<float> HealthChanged;
        public event Action<float> MaxHealthIncreased;

        public void Awake()
        {
            _animator = GetComponent<Animator>();
            _currentHealth = _maxHealth;
            
            MaxHealthIncreased?.Invoke(_maxHealth);
            HealthChanged?.Invoke(_currentHealth);
        }

        public void TryTakeDamage(float damage)
        {
            if (damage > 0 && _currentHealth > 0)
            {
                _animator.Play(PlayerAnimator.States.Hit);

                _currentHealth -= damage;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
                
                HealthChanged?.Invoke(_currentHealth);

                if (_currentHealth <= 0)
                {
                    Die();
                }
            }
        }
        
        public void SetMaxHealth(float maxHealth)
        {
            _maxHealth = maxHealth;

            MaxHealthIncreased?.Invoke(_maxHealth);
        }

        private void Die()
        {
            Debug.Log("die");
        }
    }
}