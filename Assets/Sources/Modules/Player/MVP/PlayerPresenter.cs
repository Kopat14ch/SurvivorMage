using System;

namespace Sources.Modules.Player.MVP
{
    internal class PlayerPresenter
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public event Action<float> MaxHealthChanged;
        public event Action<float> DamageScalerChanged;
        public event Action<float> SpeedChanged;

        public PlayerPresenter(PlayerModel model, PlayerView view )
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.ButtonAddMaxHealthPressed += OnButtonAddMaxHealthPressed;
            _view.ButtonAddDamageScalerPressed += OnButtonAddDamageScalerPressed;
            _view.ButtonAddSpeedPressed += OnButtonAddSpeedPressed;

            _model.MaxHealthChanged += OnMaxHealthChanged;
            _model.DamageScalerChanged += OnDamageScalerChanged;
            _model.SpeedChanged += OnSpeedChanged;
        }

        public void Disable()
        {
            _view.ButtonAddMaxHealthPressed -= OnButtonAddMaxHealthPressed;
            _view.ButtonAddDamageScalerPressed -= OnButtonAddDamageScalerPressed;
            _view.ButtonAddSpeedPressed -= OnButtonAddSpeedPressed;
        }
        
        private void OnMaxHealthChanged(float maxHealth)
        {
            _view.ChangeMaxHealthText(maxHealth);
            MaxHealthChanged?.Invoke(maxHealth);
        }

        private void OnDamageScalerChanged(float damageScaler)
        {
            _view.ChangeDamageScalerText(damageScaler);
            DamageScalerChanged?.Invoke(damageScaler);
        }

        private void OnSpeedChanged(float speed)
        {
            _view.ChangeSpeedText(speed);
            SpeedChanged?.Invoke(speed);
        }

        private void OnButtonAddMaxHealthPressed()
        {
            _model.TryAddMaxHealth();
        }
        
        private void OnButtonAddDamageScalerPressed()
        {
            _model.TryAddDamageScaler();
        }

        private void OnButtonAddSpeedPressed()
        {
            _model.TryAddSpeed();
        }
    }
}