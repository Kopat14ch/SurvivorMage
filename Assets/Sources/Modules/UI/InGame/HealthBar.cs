using UnityEngine;
using Sources.Modules.Player;
using Sources.Modules.UI.InGame.Common;
using UnityEngine.UI;

namespace Sources.Modules.UI.InGame
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : Bar
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
            Debug.Log("update");
            Slider.maxValue = value;
        }
    }
}
