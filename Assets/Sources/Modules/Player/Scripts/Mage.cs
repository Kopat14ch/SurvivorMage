using System;
using Sources.Modules.Particles.Scripts;
using Sources.Modules.Player.Scripts.Animation;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class Mage : MonoBehaviour
    {
        private const float MinDamageScaler = 1;

        [SerializeField] private ParticleSpawner _particleSpawner;

        private Animator _animator;
        private float _maxHealth = 300;
        private float _currentHealth;

        public event Action<float> HealthChanged;
        public event Action<float> MaxHealthIncreased;

        public float DamageScaler { get; private set; }

        public void Awake()
        {
            DamageScaler = MinDamageScaler;

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

        public void OnChangeDamageScaler(float damageScaler)
        {
            DamageScaler = Mathf.Clamp(damageScaler, MinDamageScaler, float.MaxValue);
        }

        private void Die()
        {
            _particleSpawner.SpawnParticle(ParticleType.MageDied, transform.position);
            Debug.Log("die");
        }
    }
}