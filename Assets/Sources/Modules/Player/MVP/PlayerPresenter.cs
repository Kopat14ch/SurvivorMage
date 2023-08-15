using System;
using UnityEngine;

namespace Sources.Modules.Player.MVP
{
    internal class PlayerPresenter
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public event Action<float> MaxHealthChanged;
        public event Action<float> DamageScalerChanged;
        public event Action<float> SpeedChanged;

        public PlayerPresenter(PlayerModel model, PlayerView view)
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
            
            _model.InvokeAll();
        }

        public void Disable()
        {
            _view.ButtonAddMaxHealthPressed -= OnButtonAddMaxHealthPressed;
            _view.ButtonAddDamageScalerPressed -= OnButtonAddDamageScalerPressed;
            _view.ButtonAddSpeedPressed -= OnButtonAddSpeedPressed;
            
            _model.MaxHealthChanged -= OnMaxHealthChanged;
            _model.DamageScalerChanged -= OnDamageScalerChanged;
            _model.SpeedChanged -= OnSpeedChanged;
        }
        
        private void OnMaxHealthChanged(float maxHealth, float increase)
        {
            _view.ChangeMaxHealthText(maxHealth, increase);
            MaxHealthChanged?.Invoke(maxHealth);
        }

        private void OnDamageScalerChanged(float damageScaler, float increase)
        {
            _view.ChangeDamageScalerText(damageScaler, increase);
            DamageScalerChanged?.Invoke(damageScaler);
        }

        private void OnSpeedChanged(float speed, float increase)
        {
            _view.ChangeSpeedText(speed, increase);
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