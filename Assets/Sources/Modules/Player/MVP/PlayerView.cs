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
        
        
        private void OnEnable()
        {
            _maxHealthButton.onClick.AddListener((() => ButtonAddMaxHealthPressed?.Invoke()));
            _addDamageButton.onClick.AddListener((() => ButtonAddDamageScalerPressed?.Invoke()));
            _addSpeedButton.onClick.AddListener((() => ButtonAddSpeedPressed?.Invoke()));
        }
        
        private void OnDisable()
        {
            _maxHealthButton.onClick.RemoveListener((() => ButtonAddMaxHealthPressed?.Invoke()));
            _addDamageButton.onClick.RemoveListener((() => ButtonAddDamageScalerPressed?.Invoke()));
            _addSpeedButton.onClick.RemoveListener((() => ButtonAddSpeedPressed?.Invoke()));
        }

        public void ChangeMaxHealthText(float maxHealth, float increase)
        {
            _currentHealthText.text = Mathf.CeilToInt(maxHealth).ToString();
            _upgradeHealthText.text =
                Mathf.CeilToInt(maxHealth + increase).ToString();
        }
        
        public void ChangeDamageScalerText(float damageScaler, float increase)
        {
            _currentDamageText.text = damageScaler.ToString("F1");
            _upgradeDamageText.text = (damageScaler + increase).ToString("F1");
        }

        public void ChangeSpeedText(float speed, float increase)
        {
            _currentSpeedText.text = speed.ToString("F1");
            _upgradeSpeedText.text = (speed + increase).ToString("F1");
        }
    }
}