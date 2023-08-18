using Sources.Modules.Player;
using Sources.Modules.Player.Scripts;
using Sources.Modules.UI.Scripts.InGame.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.PlayerUI
{
    [RequireComponent(typeof(Slider))]
    internal class HealthBar : Bar
    {
        [SerializeField] private Mage _mage;

        protected override void Awake()
        {
            Slider = GetComponent<Slider>();
            _mage.HealthChanged += ChangeValue;
            _mage.MaxHealthIncreased += UpdateMaxValue;
        }

        private void OnDisable()
        {
            _mage.HealthChanged -= ChangeValue;
            _mage.MaxHealthIncreased -= UpdateMaxValue;
        }

        private void UpdateMaxValue(float value)
        {
            Slider.maxValue = value;
        }
    }
}