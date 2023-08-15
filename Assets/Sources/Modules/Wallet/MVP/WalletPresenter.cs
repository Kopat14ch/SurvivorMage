using System.Collections.Generic;
using Sources.Modules.Player.MVP;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletPresenter
    {
        private readonly WalletModel _model;
        private readonly WalletView _view;
        private List<Coin> _coins;
        
        private CoinSpawner _spawner;
        private PlayerView _playerView;

        public WalletPresenter(WalletModel model, WalletView view, CoinSpawner spawner, PlayerView playerView)
        {
            _model = model;
            _view = view;
            _spawner = spawner;
            _playerView = playerView;
            _coins = new List<Coin>();
        }

        public void Enable()
        {
            _playerView.MaxHealthIncreasingButtonPressed += TryBuyHealthIncreasing;
            _playerView.DamageScalerIncreasingButtonPressed += TryBuyDamageScalerIncreasing;
            _playerView.SpeedIncreasingButtonPressed += TryBuySpeedIncreasing;
            
            _spawner.CoinSpawned += OnCoinSpawned;
            _model.CoinsChanged += OnCoinsChanged;
            
            _model.UpdateCoins();
        }

        public void Disable()
        {
            _playerView.MaxHealthIncreasingButtonPressed -= TryBuyHealthIncreasing;
            _playerView.DamageScalerIncreasingButtonPressed -= TryBuyDamageScalerIncreasing;
            _playerView.SpeedIncreasingButtonPressed -= TryBuySpeedIncreasing;
            
            foreach (Coin coin in _coins)
                coin.Taken -= OnCoinTaken;
            
            _spawner.CoinSpawned -= OnCoinSpawned;
            _model.CoinsChanged -= OnCoinsChanged;
        }

        public void TryBuyHealthIncreasing(int price)
        {
            if (TryBuy(price))
                _playerView.AddMaxHealth();
        }
        
        public void TryBuyDamageScalerIncreasing(int price)
        {
            if (TryBuy(price))
                _playerView.AddDamageScaler();
        }
        
        public void TryBuySpeedIncreasing(int price)
        {
            if (TryBuy(price))
                _playerView.AddSpeed();
        }

        private bool TryBuy(int price)
        {
            if (_model.IsCoinsEnough(price))
            {
                _model.TakeOffCoins(price);
                return true;
            }

            return false;
        }
        
        private void OnCoinSpawned(Coin coin)
        {
            _coins.Add(coin);
            coin.Taken += OnCoinTaken;
        }
        
        private void OnCoinTaken(Coin coin)
        {
            _model.AddCoin();
            coin.Taken -= OnCoinTaken;
        }

        private void OnCoinsChanged(int coins)
        {
            _view.ChangeCoinText(coins);
        }
    }
}
