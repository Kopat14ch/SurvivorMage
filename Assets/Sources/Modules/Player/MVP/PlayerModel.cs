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

        public event Action<float, float, bool> MaxHealthChanged;
        public event Action<float, float, bool> DamageScalerChanged;
        public event Action<float, float, bool> SpeedChanged;
        
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
            MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue, true);
            DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue, true);
            SpeedChanged?.Invoke(Speed, SpeedIncreaseValue, true);
        }

        public void TryAddMaxHealth()
        {
            bool canBeIncreased = MaxHealth + MaxHealthIncreaseValue <= MaxHealthLimit;
            bool canBeIncreasedTwice = MaxHealth + (2 * MaxHealthIncreaseValue) <= MaxHealthLimit;

            if (canBeIncreased)
            {
                MaxHealth += MaxHealthIncreaseValue;
                MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue, canBeIncreasedTwice);
            }
        }

        public void TryAddDamageScaler()
        {
            bool canBeIncreased = DamageScaler + DamageScalerIncreaseValue <= DamageScalerLimit;
            bool canBeIncreasedTwice = DamageScaler + (2 * DamageScalerIncreaseValue) <= DamageScalerLimit;

            if (canBeIncreased)
            {
                DamageScaler += DamageScalerIncreaseValue;
                DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue, canBeIncreasedTwice);
            }
        }
        
        public void TryAddSpeed()
        {
            bool canBeIncreased = Speed + SpeedIncreaseValue <= SpeedLimit;
            bool canBeIncreasedTwice = Speed + (2 * SpeedIncreaseValue) <= SpeedLimit;

            if (canBeIncreased)
            {
                Speed += SpeedIncreaseValue;
                SpeedChanged?.Invoke(Speed, SpeedIncreaseValue , canBeIncreasedTwice);
            }
        }
    }
}