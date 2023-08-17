using System;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    public class WalletModel
    {
        private const int MinAddCoin = 1;
        private const int AddCoinLimit = 5;
        private const int IncreaseCoin = 1;
        
        private int _coins;
        private int _addCoins;
        
        public event Action<int> CoinsChanged; 
        public event Action<int, int,bool> IncreaseChanged;

        public WalletModel(int coins, int coinsAdd)
        {
            _coins = coins;
            _addCoins = Mathf.Clamp(coinsAdd, MinAddCoin, AddCoinLimit);
        }

        public void InvokeAll()
        {
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin, true);
        }
        
        public void AddCoin()
        {
            _coins += _addCoins;
            CoinsChanged?.Invoke(_coins);
        }

        public bool TryBuy(int price)
        {
            if (_coins - price < 0)
                return false;

            _coins -= price;
            CoinsChanged?.Invoke(_coins);
            return true;
        }

        public void TryBuyIncrease(int price)
        {
            if (_addCoins >= AddCoinLimit || _coins - price < 0)
                return;
            
            bool canBeIncreased = _addCoins + IncreaseCoin <= AddCoinLimit;
            bool canBeIncreasedTwice = _addCoins + (IncreaseCoin + IncreaseCoin) <= AddCoinLimit;

            if (canBeIncreased)
            {
                _coins -= price;
                _addCoins += IncreaseCoin;
                CoinsChanged?.Invoke(_coins);
                IncreaseChanged?.Invoke(_addCoins, IncreaseCoin, canBeIncreasedTwice);
            }
        }
    }
}
