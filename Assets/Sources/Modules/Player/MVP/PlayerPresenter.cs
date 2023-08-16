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
            ViewEnable();
            ModelEnable();
            
            _model.InvokeAll();
        }

        public void Disable()
        {
            ViewDisable();
            ModelDisable();
        }
        
        private void OnMaxHealthChanged(float maxHealth, float increase, bool canBeIncreased)
        {
            _view.ChangeMaxHealthText(maxHealth, increase, canBeIncreased);
            MaxHealthChanged?.Invoke(maxHealth);
        }

        private void OnDamageScalerChanged(float damageScaler, float increase, bool canBeIncreased)
        {
            _view.ChangeDamageScalerText(damageScaler, increase, canBeIncreased);
            DamageScalerChanged?.Invoke(damageScaler);
        }

        private void OnSpeedChanged(float speed, float increase, bool canBeIncreased)
        {
            _view.ChangeSpeedText(speed, increase, canBeIncreased);
            SpeedChanged?.Invoke(speed);
        }

        private void ViewEnable()
        {
            _view.MaxHealthIncreasingBought += OnMaxHealthIncreasingBought;
            _view.DamageScalerIncreasingBought += OnDamageScalerIncreasingBought;
            _view.SpeedIncreasingBought += OnSpeedIncreasingBought;
        }

        private void ViewDisable()
        {
            _view.MaxHealthIncreasingBought -= OnMaxHealthIncreasingBought;
            _view.DamageScalerIncreasingBought -= OnDamageScalerIncreasingBought;
            _view.SpeedIncreasingBought -= OnSpeedIncreasingBought;
        }

        private void ModelEnable()
        {
            _model.MaxHealthChanged += OnMaxHealthChanged;
            _model.DamageScalerChanged += OnDamageScalerChanged;
            _model.SpeedChanged += OnSpeedChanged;
        }

        private void ModelDisable()
        {
            _model.MaxHealthChanged -= OnMaxHealthChanged;
            _model.DamageScalerChanged -= OnDamageScalerChanged;
            _model.SpeedChanged -= OnSpeedChanged;
        }
        
        private void OnMaxHealthIncreasingBought() => _model.TryAddMaxHealth();
        private void OnDamageScalerIncreasingBought() => _model.TryAddDamageScaler();
        private void OnSpeedIncreasingBought() => _model.TryAddSpeed();
    }
}