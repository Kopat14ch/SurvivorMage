using System;

namespace Sources.Modules.Player.MVP
{
    public class PlayerModel
    {
        private const float MaxHealthIncreaseValue = 3.5f;
        private const float DamageScalerIncreaseValue = 0.1f;
        private const float SpeedIncreaseValue = 0.4f;

        private const float MaxHealthLimit = 500;
        private const float DamageScalerLimit = 3f;
        private const float SpeedLimit = 25f;

        public event Action<float> MaxHealthChanged;
        public event Action<float> SpeedChanged;
        public event Action<float> DamageScalerChanged;

        public float MaxHealth { get; private set; }
        public float DamageScaler { get; private set; }
        public float Speed { get; private set; }

        public PlayerModel(float maxHealth, float speed, float damageScaler)
        {
            MaxHealth = maxHealth;
            Speed = speed;
            DamageScaler = damageScaler;
        }

        public void TryAddMaxHealth()
        {
            if (MaxHealth >= MaxHealthLimit)
                return;
            
            MaxHealth += MaxHealthIncreaseValue;
            MaxHealthChanged?.Invoke(MaxHealth);
        }

        public void TryAddDamageScaler()
        {
            if (DamageScaler >= DamageScalerLimit)
                return;

            DamageScaler += DamageScalerIncreaseValue;
            DamageScalerChanged?.Invoke(DamageScaler);
        }
        
        public void TryAddSpeed()
        {
            if (Speed >= SpeedLimit)
                return;
            
            Speed += SpeedIncreaseValue;
            SpeedChanged?.Invoke(Speed);
        }
    }
}