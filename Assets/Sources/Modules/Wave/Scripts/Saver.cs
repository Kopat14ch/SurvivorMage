using System;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Wave.Scripts
{
    public static class Saver
    {
        private static bool s_isInitialized;
        private static event Action s_OnLoaded;
        private static WaveData s_waveData;

        public static void Init(Action onLoaded)
        {
            if (s_isInitialized)
            {
                s_OnLoaded = onLoaded;
                s_isInitialized = true;
                TryLoadWave();
            }
            else
            {
                onLoaded?.Invoke();
            }
        }

        public static void SaveWaveData(WaveData waveData)
        {
            s_waveData = waveData;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(s_waveData));
        }

        public static WaveData GetWaveData()
        {
#if UNITY_EDITOR
            return null;
#endif

            return s_waveData;
        }

        private static void TryLoadWave() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadWave);

        private static void LoadWave(string json)
        {
            s_waveData = JsonUtility.FromJson<WaveData>(json);
            
            s_OnLoaded?.Invoke();
        }
    }
}