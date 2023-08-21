using System;
using Sources.Modules.UI.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Modules.Player.Scripts.UI
{
    [RequireComponent(typeof(Panel))]
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private Mage _mage;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _rewardButton;

        private const string GameScene = nameof(GameScene);
        
        private Panel _panel;

        public event Action Rewarded;

        private void Awake()
        {
            _panel = GetComponent<Panel>();
            _panel.TurnOff();
        }

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
            _panel.TurnOff();
            _mage.UpdateCurrentHealth();
            _mage.SetStartPosition();
            Rewarded?.Invoke();
        }
    }
}