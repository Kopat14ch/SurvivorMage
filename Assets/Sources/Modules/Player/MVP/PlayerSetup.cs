using UnityEngine;

namespace Sources.Modules.Player.MVP
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private PlayerView _view;
        private PlayerPresenter _presenter;
        private Mage _mage;

        private const float BaseMaxHealth = 125;
        private const float BaseSpeed = 15f;
        private const float DamageScaler = 1f;

        public void Init(Mage mage)
        {
            _view = GetComponent<PlayerView>();
            
            PlayerModel playerModel = new (BaseMaxHealth,BaseSpeed,DamageScaler);
            _presenter = new PlayerPresenter(playerModel, _view);
            _mage = mage;
            
            _playerMovement.SetSpeed(playerModel.Speed);
            _mage.SetMaxHealth(playerModel.MaxHealth);
        }

        private void OnEnable()
        {
            _presenter.Enable();

            _presenter.MaxHealthChanged += _mage.SetMaxHealth;
            _presenter.SpeedChanged += _playerMovement.SetSpeed;
        }

        private void OnDisable()
        {
            _presenter.MaxHealthChanged -= _mage.SetMaxHealth;
            _presenter.SpeedChanged -= _playerMovement.SetSpeed;
            
            _presenter.Disable();
        }
    }
}