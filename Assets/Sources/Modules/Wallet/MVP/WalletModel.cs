using System;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletModel
    {
        private const int BaseAddValue = 1;
        private const int MinMultiplier = 0;
        private const int MaxMultiplier = 5;
        private const int AddMultiplier = 1;
        
        private int _coins;
        private int _coinMultiplier;
        
        public event Action<int> CoinsChanged; 
        public event Action<int, int> MultiplierChanged;

        public WalletModel(int coins, int coinMultiplier)
        {
            _coins = coins;
            _coinMultiplier = Mathf.Clamp(coinMultiplier, MinMultiplier, MaxMultiplier);
        }

        public void UpdateCoins()
        {
            CoinsChanged?.Invoke(_coins);
        }
        
        public void AddCoin()
        {
            Debug.Log("ADD");
            _coins += BaseAddValue + _coinMultiplier;
            CoinsChanged?.Invoke(_coins);
        }

        public bool TryBuy(int price)
        {
            if (_coins - price < 0)
                return false;
            
            CoinsChanged?.Invoke(_coins);
            return true;
        }

        public bool TryAddMultiplier(int price)
        {
            if (_coinMultiplier >= MaxMultiplier || _coins - price < 0)
                return false;

            _coinMultiplier += AddMultiplier;
            MultiplierChanged?.Invoke(_coinMultiplier, AddMultiplier);
            return true;
        }
    }
}
