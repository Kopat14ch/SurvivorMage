using Sources.Modules.UI.Scripts.InGame.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Wave.Scripts.UI
{
    internal class WaveIndicatorBar : Bar
    {
        [SerializeField] private WaveGenerator _waveGenerator;

        protected override void Awake()
        {
            Slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            _waveGenerator.WaveStarted += UpdateMaxValue;
            _waveGenerator.UnitDied += DecreaseValueByOne;
        }

        private void OnDisable()
        {
            _waveGenerator.WaveStarted -= UpdateMaxValue;
            _waveGenerator.UnitDied -= DecreaseValueByOne;
        }

        private void UpdateMaxValue(int value)
        {
            Slider.maxValue = value;
            ChangeValue(value);
        }

        private void DecreaseValueByOne()
        {
            float currentBarValue = Slider.value;
            ChangeValue(--currentBarValue);
        }
    }
}
