using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Wave.Scripts.UI
{
    public class WaveEndUI : MonoBehaviour
    {
        [SerializeField] private WaveGenerator _waveGenerator;
        [SerializeField] private Button _nextWaveButton;

        public event Action NextWaveButtonPressed;

        private void Start()
        {
            _nextWaveButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _waveGenerator.WaveEnded += OnWaveEnded;
            _nextWaveButton.onClick.AddListener(OnNextWaveButtonClick);
        }
        
        private void OnDisable()
        {
            _waveGenerator.WaveEnded -= OnWaveEnded;
            _nextWaveButton.onClick.RemoveListener(OnNextWaveButtonClick);
        }

        private void OnWaveEnded()
        {
            _nextWaveButton.gameObject.SetActive(true);
        }

        private void OnNextWaveButtonClick()
        {
            _waveGenerator.StartWave();
            _nextWaveButton.gameObject.SetActive(false);
            NextWaveButtonPressed?.Invoke();
        }
    }
}