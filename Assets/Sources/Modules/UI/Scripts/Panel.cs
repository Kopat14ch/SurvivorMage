using System;
using Agava.YandexGames;
using Sources.Modules.UI.Scripts.LeaderBoard;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private bool _isInGamePanel = false;
        [SerializeField] private bool _isEnabled = false;
        [SerializeField] private bool _isPausePanel = false;
        [SerializeField] private bool _isLeaderboard;
        [SerializeField] private bool _isAuthorization;
        [SerializeField] private bool _isWorkshop;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;

        public event Action<Panel> Enabled;
        public event Action<Panel> Disabled;

        public bool IsInGamePanel => _isInGamePanel;
        public bool IsEnabled => _isEnabled;
        public bool IsLeaderboard => _isLeaderboard;
        public bool IsAuthorization => _isAuthorization;
        
        private void OnEnable()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(TurnOff);
            
            if (_openButton != null)
                _openButton.onClick.AddListener(TurnOn);
        }

        private void OnDisable()
        {
            if (_closeButton != null)
                _closeButton.onClick.RemoveListener(TurnOff);
            
            if (_openButton != null)
                _openButton.onClick.RemoveListener(TurnOn);
        }

        public void TurnOn()
        {
            ShowCanvas();
            
            Enabled?.Invoke(this);
        }
        
        public void TurnOnWithoutInvoke()
        {
            ShowCanvas();
        }
        
        public void TurnOff()
        {
            HideCanvas();
            
            Disabled?.Invoke(this);
        }
        
        public void TurnOffWithoutInvoke()
        {
            HideCanvas();
        }

        private void ShowCanvas()
        {
            _isEnabled = true;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            
            if (_isPausePanel)
                Time.timeScale = 0;

            if (_isLeaderboard && PlayerAccount.IsAuthorized)
            {
                LeaderList leaderboard = GetComponent<LeaderList>();
                leaderboard.ShowResults();
            }
        }

        private void HideCanvas()
        {
            _isEnabled = false;
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            
            if (_isPausePanel)
                Time.timeScale = 1;
            
            if (_isWorkshop)
                _yandex.ShowInterstitial();
            
            if (_isLeaderboard && PlayerAccount.IsAuthorized)
            {
                LeaderList leaderboard = GetComponent<LeaderList>();
                leaderboard.Clear();
            }
        }
    }
}
