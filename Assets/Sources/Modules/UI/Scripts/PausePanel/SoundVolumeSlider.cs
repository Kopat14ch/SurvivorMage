using Sources.Modules.Sound.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts.PausePanel
{
    public class SoundVolumeSlider : MonoBehaviour
    {
        [SerializeField] private SoundContainer _soundContainer;
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
            _soundContainer.ChangeVolume(volume);
        }
    }
}
