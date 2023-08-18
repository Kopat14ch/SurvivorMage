using System;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    public class WalletModel
    {
        private const int IncreaseCoin = 1;
        
        private int _coins;
        private int _addCoins;
        
        public event Action<int> CoinsChanged; 
        public event Action<int, int> IncreaseChanged;

        public WalletModel(int coins, int addCoins)
        {
            _coins = coins;
            _addCoins = addCoins;
        }

        public void InvokeAll()
        {
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin);
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
            if (_coins - price < 0)
                return;

            _coins -= price;
            _addCoins += IncreaseCoin;
            CoinsChanged?.Invoke(_coins);
            IncreaseChanged?.Invoke(_addCoins, IncreaseCoin);
        }
    }
}
