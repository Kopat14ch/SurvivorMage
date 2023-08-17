using UnityEngine;

namespace Sources.Modules.Player.Scripts.MVP
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private const float BaseMaxHealth = 125;
        private const float BaseSpeed = 15f;
        private const float DamageScaler = 1f;
        
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private Mage _mage;
        private PlayerModel _playerModel;


        public void Init(Mage mage)
        {
            _view = GetComponent<PlayerView>();
            
            _playerModel = new (BaseMaxHealth,BaseSpeed,DamageScaler);
            _presenter = new PlayerPresenter(_playerModel, _view);
            _mage = mage;
            
            _playerMovement.SetSpeed(_playerModel.Speed);
            _mage.SetMaxHealth(_playerModel.MaxHealth);
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