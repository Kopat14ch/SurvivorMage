using Sources.Modules.CoinFactory.Scripts;
using Sources.Modules.Player.Scripts.MVP;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts.MVP
{
    [RequireComponent(typeof(WalletView))]
    public class WalletSetup : MonoBehaviour
    {
        [SerializeField] private CoinSpawner _spawner;

        private const int BaseCoins = 200;
        private const int BaseIncrease = 0;
        
        private WalletView _view;
        private WalletPresenter _presenter;

        private WalletModel _model;

        public void Init(PlayerView playerView)
        {
            _view = GetComponent<WalletView>();

            _model = new (BaseCoins, BaseIncrease);
            _presenter = new (_model, _view, _spawner, playerView);
        }

        private void OnEnable() => _presenter.Enable();
        private void OnDisable() => _presenter.Disable();
    }
}
