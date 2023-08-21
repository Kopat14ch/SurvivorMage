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

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(ChangeVolume);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(ChangeVolume);
        }

        private void ChangeVolume(float volume)
        {
            _backgroundSound.ChangeVolume(volume);
        }
    }
}
