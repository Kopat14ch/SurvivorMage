using Agava.WebUtility;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class AudioListenerSwitch : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;

        private void OnEnable()
        {
            _yandex.AdOpened += OnInterstitialOpened;
            _yandex.AdClosed += OnInterstitialClosed;
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }

        private void OnDisable()
        {
            _yandex.AdOpened -= OnInterstitialOpened;
            _yandex.AdClosed -= OnInterstitialClosed;
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }

        private void OnInterstitialOpened()
        {
            AudioListener.pause = true;
            AudioListener.volume = 0;
        }

        private void OnInterstitialClosed(bool wasShown)
        {
            AudioListener.pause = false;
            AudioListener.volume = 1;
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            AudioListener.pause = inBackground;
            AudioListener.volume = inBackground ? 0f : 1f;
        }
    }
}
