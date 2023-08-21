using System;

namespace Sources.Modules.Player.Scripts.MVP
{
    internal class PlayerModel
    {
        private const float MaxHealthIncreaseValue = 3.5f;
        private const float DamageScalerIncreaseValue = 0.1f;
        private const float SpeedIncreaseValue = 0.1f;

        public event Action<float, float> MaxHealthChanged;
        public event Action<float, float> DamageScalerChanged;
        public event Action<float, float> SpeedChanged;
        
        public float MaxHealth { get; private set; }
        public float DamageScaler { get; private set; }
        public float Speed { get; private set; }

        public PlayerModel(float maxHealth, float speed, float damageScaler)
        {
            MaxHealth = maxHealth;
            Speed = speed;
            DamageScaler = damageScaler;
        }

        public void InvokeAll()
        {
            MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue);
            DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue);
            SpeedChanged?.Invoke(Speed, SpeedIncreaseValue);
        }

        public void AddMaxHealth()
        {
            MaxHealth += MaxHealthIncreaseValue;
            MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue);
        }

        public void AddDamageScaler()
        {
            DamageScaler += DamageScalerIncreaseValue;
            DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue);
        }
        
        public void TryAddSpeed()
        {
            Speed += SpeedIncreaseValue;
            SpeedChanged?.Invoke(Speed, SpeedIncreaseValue);
        }
    }
}