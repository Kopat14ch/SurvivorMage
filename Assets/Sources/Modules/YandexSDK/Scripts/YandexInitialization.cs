using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexInitialization : MonoBehaviour
    {
        public event Action Initialized;
        
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }
        
        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
        }
        
        private IEnumerator Start()
        {
            #if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
            #endif

            yield return YandexGamesSdk.Initialize(OnInitialized);
        }
        
        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
        }
        
        private void OnInitialized()
        {
            Initialized?.Invoke();
            InterstitialAd.Show(OnAdOpened, OnAdClosed);
        }
        
        private void OnAdOpened()
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            AudioListener.volume = 0;
        }

        private void OnAdClosed(bool showed)
        {
            Time.timeScale = 1;
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
