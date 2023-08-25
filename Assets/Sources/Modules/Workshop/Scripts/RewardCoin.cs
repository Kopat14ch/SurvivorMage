using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Sources.Modules.Workshop.Scripts
{
    [RequireComponent(typeof(Button))]
    public class RewardCoin : MonoBehaviour
    {
        private Button _button;
        private const int MinCoins = 1;
        private const int EasyCoins = 5;
        private const int MediumCoins = 25;
        private const int MaxCoins = 50;
        private const int MaxCoinsRandomValue = 97;
        private const int MediumCoinsRandomValue = 85;
        private const int MaxRandomValue = 100;

        public event Action<int> Rewarded;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            int randomValue = Random.Range(0, MaxRandomValue);
            int randomCoins;

            if (randomValue > MaxCoinsRandomValue)
            {
                randomCoins = Random.Range(MediumCoins, MaxCoins);
                Debug.Log($"MAX: {randomCoins}");
            }
            else if (randomValue > MediumCoinsRandomValue)
            {
                randomCoins = Random.Range(EasyCoins, MediumCoins);
                Debug.Log($"MEDIUM: {randomCoins}");
            }
            else
            {
                randomCoins = Random.Range(MinCoins, EasyCoins);
                Debug.Log($"EASY: {randomCoins}");
            }
            
            Rewarded?.Invoke(randomCoins);
        }
    }
}
