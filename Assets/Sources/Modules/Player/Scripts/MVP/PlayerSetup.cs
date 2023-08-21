using UnityEngine;

namespace Sources.Modules.Player.Scripts.MVP
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private const float BaseMaxHealth = 60;
        private const float BaseSpeed = 7f;
        private const float DamageScaler = 1f;
        
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private Mage _mage;
        private PlayerModel _playerModel;

        public void Init(Mage mage)
        {
            _view = GetComponent<PlayerView>();
            _mage = mage;
            
            SetDefault();
        }

        private void OnEnable()
        {
            _presenter.Enable();

            _presenter.MaxHealthChanged += _mage.SetMaxHealth;
            _presenter.DamageScalerChanged += _mage.OnChangeDamageScaler;
            _presenter.SpeedChanged += _playerMovement.SetSpeed;
        }

        private void OnDisable()
        {
            _presenter.MaxHealthChanged -= _mage.SetMaxHealth;
            _presenter.DamageScalerChanged -= _mage.OnChangeDamageScaler;
            _presenter.SpeedChanged -= _playerMovement.SetSpeed;

            _presenter.Disable();
        }

        private void SetDefault()
        {
            _playerModel = new (BaseMaxHealth,BaseSpeed,DamageScaler);
            _presenter = new PlayerPresenter(_playerModel, _view);
            
            _playerMovement.SetSpeed(_playerModel.Speed);
            _mage.SetMaxHealth(_playerModel.MaxHealth);
            _mage.OnChangeDamageScaler(_playerModel.DamageScaler);
        }
    }
}