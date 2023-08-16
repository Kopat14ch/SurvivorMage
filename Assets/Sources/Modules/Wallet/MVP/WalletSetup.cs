using Sources.Modules.Player.MVP;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    [RequireComponent(typeof(WalletView))]
    public class WalletSetup : MonoBehaviour
    {
        [SerializeField] private CoinSpawner _spawner;
        [SerializeField] private PlayerView _playerView;

        private WalletView _view;
        private WalletPresenter _presenter;

        private const int BaseCoins = 200;
        private const int BaseMultiplier = 0;

        private void Awake()
        {
            _view = GetComponent<WalletView>();
            
            WalletModel walletModel = new (BaseCoins, BaseMultiplier);
            _presenter = new (walletModel, _view, _spawner, _playerView);
        }

        private void OnEnable() => _presenter.Enable();
        private void OnDisable() => _presenter.Disable();
    }
}
