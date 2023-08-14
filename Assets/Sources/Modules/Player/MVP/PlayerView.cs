using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Player.MVP
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Button _maxHealthButton;
        [SerializeField] private Button _addDamageButton;
        [SerializeField] private Button _addSpeedButton;
        [SerializeField] private Text _healthText;
        [SerializeField] private Text _damageText;
        [SerializeField] private Text _speedText;
        
        public event Action ButtonAddMaxHealthPressed;
        public event Action ButtonAddDamageScalerPressed;
        public event Action ButtonAddSpeedPressed;

        private void Awake()
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

        public void ChangeMaxHealthText(float maxHealth)
        {
            
        }
        
        public void ChangeDamageScalerText(float damageScaler)
        {
            
        }
        
        public void ChangeSpeedText(float speed)
        {
            
        }
    }
}