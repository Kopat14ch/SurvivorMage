using System;
using Sources.Modules.Training.Enums;
using Sources.Modules.UIElementTraining.Scripts;
using UnityEngine;

namespace Sources.Modules.Training.Scripts
{
    public class TrainingView : MonoBehaviour
    {
        [SerializeField] private TrainingUI[] _trainingUis;
        [SerializeField] private UIElement _playerHealthUI;
        [SerializeField] private UIElement _playerWalletUI;
        [SerializeField] private UIElement _waveBackground;
        [SerializeField] private UIElement _waveCount;
        [SerializeField] private UIElement _nextWaveButton;
        [SerializeField] private UIElement _settingButton;
        [SerializeField] private UIElement _leaderBoardButton;

        private int _currentSlideIndex = 1;

        public event Action RequestNextButtonEnable;
        public event Action RequestNextButtonDisable;
        public event Action RequestExitButtonEnable;
        public event Action RequestExitButtonDisable;

        public void NextSlide()
        {
            if (_currentSlideIndex >= _trainingUis.Length)
            {
                gameObject.SetActive(false);
                return;
            }

            if (_currentSlideIndex > 0)
            {
                switch (_currentSlideIndex)
                {
                    case (int) TrainingObjects.PlayerHealth:
                        _playerHealthUI.Enable();
                        break;
                    case (int) TrainingObjects.PlayerWallet:
                        _playerWalletUI.Enable();
                        break;
                    case (int) TrainingObjects.GoToShop:
                        RequestNextButtonDisable?.Invoke();
                        break;
                    case (int) TrainingObjects.Upgrades:
                        RequestExitButtonDisable?.Invoke();
                        break;
                    case (int) TrainingObjects.CloseShop:
                        RequestExitButtonEnable?.Invoke();
                        break;
                }

                _trainingUis[_currentSlideIndex - 1].gameObject.SetActive(false);
            }


            _trainingUis[_currentSlideIndex].gameObject.SetActive(true);
            _currentSlideIndex++;
        }


        public void TryNextSlide()
        {
            if ((int)TrainingObjects.GoToShop == _currentSlideIndex - 1)
                NextSlide();
        }
        
        public void EnableButton()
        {
            RequestNextButtonEnable?.Invoke();
        }
    }
}
