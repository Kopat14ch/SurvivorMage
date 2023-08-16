using System.Collections.Generic;
using Sources.Modules.Player.MVP;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletPresenter
    {
        private readonly WalletModel _model;
        private readonly WalletView _view;
        private readonly List<Coin> _coins;
        
        private CoinSpawner _coinSpawner;
        private PlayerView _playerView;

        public WalletPresenter(WalletModel model, WalletView view, CoinSpawner coinSpawner, PlayerView playerView)
        {
            _model = model;
            _view = view;
            _coinSpawner = coinSpawner;
            _playerView = playerView;
            _coins = new List<Coin>();
        }

        public void Enable()
        {
            PlayerViewEnable();

            if (_coins.Count > 0)
                foreach (var coin in _coins)
                    coin.Taken += OnCoinTaken;

            _coinSpawner.CoinSpawned += OnCoinSpawned;
            _model.CoinsChanged += OnCoinsChanged;
            
            _model.UpdateCoins();
        }

        public void Disable()
        {
            PlayerViewDisable();
            
            foreach (Coin coin in _coins)
                coin.Taken -= OnCoinTaken;
            
            _coinSpawner.CoinSpawned -= OnCoinSpawned;
            _model.CoinsChanged -= OnCoinsChanged;
        }

        private void OnMaxHealthIncreasingButtonPressed(int price)
        {
            if (_model.TryBuy(price))
                _playerView.AddMaxHealth();
        }
        
        private void OnDamageScalerIncreasingButtonPressed(int price)
        {
            if (_model.TryBuy(price))
                _playerView.AddDamageScaler();
        }
        
        private void OnSpeedIncreasingButtonPressed(int price)
        {
            if (_model.TryBuy(price))
                _playerView.AddSpeed();
        }

        private void OnCoinCostIncreasingButtonPressed(int price)
        {
            if (_model.TryAddMultiplier(price))
                _playerView.AddCoinCost();
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

        private void PlayerViewEnable()
        {
            _playerView.MaxHealthIncreasingButtonPressed += OnMaxHealthIncreasingButtonPressed;
            _playerView.DamageScalerIncreasingButtonPressed += OnDamageScalerIncreasingButtonPressed;
            _playerView.SpeedIncreasingButtonPressed += OnSpeedIncreasingButtonPressed;
            _playerView.CoinCostIncreasingButtonPressed += OnCoinCostIncreasingButtonPressed;
        }

        private void PlayerViewDisable()
        {
            _playerView.MaxHealthIncreasingButtonPressed -= OnMaxHealthIncreasingButtonPressed;
            _playerView.DamageScalerIncreasingButtonPressed -= OnDamageScalerIncreasingButtonPressed;
            _playerView.SpeedIncreasingButtonPressed -= OnSpeedIncreasingButtonPressed;
            _playerView.CoinCostIncreasingButtonPressed -= OnCoinCostIncreasingButtonPressed;
        }
    }
}
