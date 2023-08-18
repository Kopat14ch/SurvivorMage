using System;

namespace Sources.Modules.Player.Scripts.MVP
{
    internal class PlayerModel
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
            bool canBeIncreased = CanIncreased(MaxHealth, MaxHealthIncreaseValue, MaxHealthLimit);
            bool canBeIncreasedTwice = CanBeIncreasedTwice(MaxHealth, MaxHealthIncreaseValue, MaxHealthLimit);

            if (canBeIncreased)
            {
                MaxHealth += MaxHealthIncreaseValue;
                MaxHealthChanged?.Invoke(MaxHealth, MaxHealthIncreaseValue, canBeIncreasedTwice);
            }
        }

        public void TryAddDamageScaler()
        {
            bool canBeIncreased = CanIncreased(DamageScaler, DamageScalerIncreaseValue, DamageScalerLimit);
            bool canBeIncreasedTwice = CanBeIncreasedTwice(DamageScaler, DamageScalerIncreaseValue, DamageScalerLimit);

            if (canBeIncreased)
            {
                DamageScaler += DamageScalerIncreaseValue;
                DamageScalerChanged?.Invoke(DamageScaler, DamageScalerIncreaseValue, canBeIncreasedTwice);
            }
        }
        
        public void TryAddSpeed()
        {
            bool canBeIncreased = CanIncreased(Speed, SpeedIncreaseValue, SpeedLimit);
            bool canBeIncreasedTwice = CanBeIncreasedTwice(Speed, SpeedIncreaseValue, SpeedLimit);

            if (canBeIncreased)
            {
                Speed += SpeedIncreaseValue;
                SpeedChanged?.Invoke(Speed, SpeedIncreaseValue , canBeIncreasedTwice);
            }
        }

        private bool CanIncreased(float value, float increaseValue, float limit) => value + increaseValue <= limit;

        private bool CanBeIncreasedTwice(float value, float increaseValue, float limit) => value + (increaseValue + increaseValue) <= limit;
    }
}