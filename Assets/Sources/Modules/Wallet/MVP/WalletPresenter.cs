using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletPresenter : MonoBehaviour
    {
        private readonly WalletModel _model;
        private readonly WalletView _view;
        private readonly List<Coin> _coins;

        public WalletPresenter(WalletModel model, WalletView view, List<Coin> coins)
        {
            _model = model;
            _view = view;
            _coins = coins;
        }

        public void Enable()
        {
            foreach (var coin in _coins)
                coin.Taken += OnCoinTaken;

            _model.CoinsChanged += OnCoinsChanged;
        }

        public void Disable()
        {
            foreach (var coin in _coins)
                coin.Taken -= OnCoinTaken;
            
            _model.CoinsChanged -= OnCoinsChanged;
        }

        private void OnCoinTaken()
        {
            _model.AddCoin();
        }

        private void OnCoinsChanged(float coins)
        {
            _view.ChangeCoinText(coins);
        }
    }
}
