using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Sources.Modules.Player.MVP
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Button _maxHealthButton;
        [SerializeField] private Button _addDamageButton;
        [SerializeField] private Button _addSpeedButton;
        [SerializeField] private TMP_Text _currentHealthText;
        [SerializeField] private TMP_Text _upgradeHealthText;
        [SerializeField] private TMP_Text _currentDamageText;
        [SerializeField] private TMP_Text _upgradeDamageText;
        [SerializeField] private TMP_Text _currentSpeedText;
        [SerializeField] private TMP_Text _upgradeSpeedText;
        
        public event Action ButtonAddMaxHealthPressed;
        public event Action ButtonAddDamageScalerPressed;
        public event Action ButtonAddSpeedPressed;

        private PlayerModel _playerModel;
        
        private void Awake()
        {
            _maxHealthButton.onClick.AddListener((() => ButtonAddMaxHealthPressed?.Invoke()));
            _addDamageButton.onClick.AddListener((() => ButtonAddDamageScalerPressed?.Invoke()));
            _addSpeedButton.onClick.AddListener((() => ButtonAddSpeedPressed?.Invoke()));
        }

        private void OnEnable()
        {
            ChangeMaxHealthText(_playerModel.MaxHealth);
            ChangeDamageScalerText(_playerModel.DamageScaler);
            ChangeSpeedText(_playerModel.Speed);
        }

        private void OnDisable()
        {
            _maxHealthButton.onClick.RemoveListener((() => ButtonAddMaxHealthPressed?.Invoke()));
            _addDamageButton.onClick.RemoveListener((() => ButtonAddDamageScalerPressed?.Invoke()));
            _addSpeedButton.onClick.RemoveListener((() => ButtonAddSpeedPressed?.Invoke()));
        }

        public void SetPlayerModel(PlayerModel model)
        {
            _playerModel = model;
        }
        
        public void ChangeMaxHealthText(float maxHealth)
        {
            _currentHealthText.text = Mathf.CeilToInt(maxHealth).ToString();
            _upgradeHealthText.text =
                Mathf.CeilToInt(_playerModel.MaxHealthIncreaseValue + maxHealth).ToString();
        }
        
        public void ChangeDamageScalerText(float damageScaler)
        {
            _currentDamageText.text = Mathf.CeilToInt(damageScaler).ToString();
            _upgradeDamageText.text =
                Mathf.CeilToInt(damageScaler + _playerModel.DamageScalerIncreaseValue).ToString();
        }

        public void ChangeSpeedText(float speed)
        {
            _currentSpeedText.text = speed.ToString("F1");
            _upgradeSpeedText.text = (speed + _playerModel.SpeedIncreaseValue).ToString("F1");
        }
    }
}