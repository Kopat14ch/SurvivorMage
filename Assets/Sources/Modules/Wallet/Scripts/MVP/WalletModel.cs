using System;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    public class WalletModel
    {
        private const int IncreaseCoin = 1;
        private const int MinAddCoin = 1;
        
        private int _coins;
        private int _addCoins;
        
        public event Action<int> CoinsChanged; 
        public event Action<int, int> IncreaseChanged;

        public WalletModel(int coins, int addCoins)
        {
            _coins = Saver.GetWallet()?.Coins ?? coins;
            _addCoins = Mathf.Clamp(addCoins, MinAddCoin, Int32.MaxValue);
        }

        public void InvokeAll()
        {
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin);
        }
        
        public void AddCoin(int value = -1)
        {
            if (value > 0)
                _coins += value;
            else
                _coins += _addCoins;

            Saver.SaveWallet(_coins);
            CoinsChanged?.Invoke(_coins);
        }

        public bool TryBuy(int price)
        {
            if (_coins - price < 0)
                return false;

            _coins -= price;
            Saver.SaveWallet(_coins);
            CoinsChanged?.Invoke(_coins);
            return true;
        }

        public void TryBuyIncrease(int price)
        {
            if (_coins - price < 0)
                return;

            _coins -= price;
            _addCoins += IncreaseCoin;
            Saver.SaveWallet(_coins);
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin);
        }
    }
}
