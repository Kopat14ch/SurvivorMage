using System;
using System.Collections;
using Agava.WebUtility;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Modules.YandexSDK.Scripts
{
    public class YandexInitialization : MonoBehaviour
    {
        private const string GameScene = nameof(GameScene);
        
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
            InterstitialAd.Show(OnAdOpened, OnAdClosed);
            SceneManager.LoadScene(GameScene);
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
