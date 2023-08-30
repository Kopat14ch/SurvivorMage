using System;
using Sources.Modules.Sound.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts.PausePanel
{
    public class MusicVolumeSlider : MonoBehaviour
    {
        [SerializeField] private BackgroundSound _backgroundSound;
        [SerializeField] private Slider _slider;
        [SerializeField] private Button _volumeButton;
        [SerializeField] private CanvasGroup _redLine;

        private const float MinVolume = 0;
        private const float MaxVolume = 1;
        private const float AlphaRedLineDisabled = 0;
        private const float AlphaRedLineEnabled = 1;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
            _volumeButton.onClick.AddListener(OnButtonPressed);
            TryChangeButtonView();
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
            _volumeButton.onClick.RemoveListener(OnButtonPressed);
        }

        private void OnButtonPressed()
        {
            if (_slider.value == MinVolume)
                SetVolumeByButton(MaxVolume, AlphaRedLineDisabled);
            else
                SetVolumeByButton(MinVolume, AlphaRedLineEnabled);
        }

        private void SetVolumeByButton(float volume, float alphaRedLine)
        {
            _slider.value = volume;
            ChangeVolume(volume);
            _redLine.alpha = alphaRedLine;
        }
        
        private void TryChangeButtonView() =>
            _redLine.alpha = _slider.value == MinVolume ? AlphaRedLineEnabled : AlphaRedLineDisabled;
        
        private void ChangeVolume(float volume)
        {
            _backgroundSound.ChangeVolume(volume);
            TryChangeButtonView();
        }
    }
}
