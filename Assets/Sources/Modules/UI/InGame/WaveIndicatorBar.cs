using Sources.Modules.UI.InGame.Common;
using Sources.Modules.Wave;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.InGame
{
    public class WaveIndicatorBar : Bar
    {
        [SerializeField] private WaveGenerator _waveGenerator;

        protected override void Awake()
        {
            Slider = GetComponent<Slider>();
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
