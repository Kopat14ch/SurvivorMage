using System;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletModel
    {
        private const int BaseAddValue = 1;
        private const float MinMultiplier = 1;
        private const float MaxMultiplier = 3;
        private const float AddMultiplier = 0.1f;
        
        private float _coins;
        private float _coinMultiplier;

        public event Action<float> CoinsChanged; 
        public event Action<float, float> MultiplierChanged;

        public WalletModel(float coins, float coinMultiplier)
        {
            _coins = coins;
            _coinMultiplier = Mathf.Clamp(coinMultiplier, MinMultiplier, MaxMultiplier);
        }

        public void AddCoin()
        {
            _coins += BaseAddValue * _coinMultiplier;
            CoinsChanged?.Invoke(_coins);
        }

        public void TryAddMultiplier()
        {
            if (_coinMultiplier >= MaxMultiplier)
                return;

            _coinMultiplier += AddMultiplier;
            MultiplierChanged?.Invoke(_coinMultiplier, AddMultiplier);
        }
    }
}
