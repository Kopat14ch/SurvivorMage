using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    [RequireComponent(typeof(WalletView))]
    public class WalletSetup : MonoBehaviour
    {
        private List<Coin> _coins;
        private WalletView _view;
        private WalletPresenter _presenter;

        private const float BaseCoins = 500;
        private const float BaseMultiplier = 1;

        private void Awake()
        {
            _view = GetComponent<WalletView>();
            
            WalletModel walletModel = new (BaseCoins, BaseMultiplier);
            _presenter = new (walletModel, _view, _coins);
        }

        private void OnEnable() => _presenter.Enable();
        private void OnDisable() => _presenter.Disable();
    }
}
