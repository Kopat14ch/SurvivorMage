using System;
using Sources.Modules.UI.Scripts;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Modules.Player.Scripts.UI
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private Mage _mage;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private Panel _panel;
        
        private const string GameScene = nameof(GameScene);

        public event Action Rewarded;

        private void OnEnable()
        {
            _mage.Died += _panel.TurnOn;
            
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _rewardButton.onClick.AddListener(OnRewardButtonClick);
        }

        private void OnDisable()
        {
            _mage.Died -= _panel.TurnOn;
            
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _rewardButton.onClick.RemoveListener(OnRewardButtonClick);
        }

        private void OnRestartButtonClick()
        {
            SceneManager.LoadScene(GameScene);
        }

        private void OnRewardButtonClick()
        {
            if (_yandex.IsInitialized)
                _yandex.ShowVideo(OnRewarded);
        }

        private void OnRewarded()
        {
            _panel.TurnOff();
            _mage.UpdateCurrentHealth();
            _mage.SetStartPosition();
            Rewarded?.Invoke();
        }
    }
}