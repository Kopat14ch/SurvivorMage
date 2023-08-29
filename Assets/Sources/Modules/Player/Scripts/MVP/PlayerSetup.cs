using UnityEngine;

namespace Sources.Modules.Player.Scripts.MVP
{
    [RequireComponent(typeof(PlayerView))]
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;

        private const float BaseMaxHealth = 10;
        private const float BaseSpeed = 6.8f;
        private const float DamageScaler = 1f;
        
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private Mage _mage;
        private PlayerData _data;
        private PlayerModel _model;

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
        
        public void Init(Mage mage)
        {
            _view = GetComponent<PlayerView>();
            _mage = mage;
            
            SetProperties();
        }

        public void SetDefault()
        {
            _data.Speed = BaseSpeed;
            _data.DamageScaler = DamageScaler;
            _data.MaxHealth = BaseMaxHealth;
            
            _model.SetNewProperties(_data.MaxHealth,_data.DamageScaler,_data.Speed);
            
            UpdateProperties();
        }

        private void SetProperties()
        { 
            _data = PlayerSaver.Instance.GetData();

            if (_data is {Speed: >= BaseSpeed, MaxHealth: >= BaseMaxHealth, DamageScaler: >= DamageScaler})
                _model = new (_data.MaxHealth, _data.Speed, _data.DamageScaler);
            else
                _model = new (BaseMaxHealth,BaseSpeed,DamageScaler);

            _presenter = new PlayerPresenter(_model, _view);
            
            UpdateProperties();
        }

        private void UpdateProperties()
        {
            _playerMovement.SetSpeed(_model.Speed);
            _mage.SetMaxHealth(_model.MaxHealth);
            _mage.OnChangeDamageScaler(_model.DamageScaler);
        }
    }
}