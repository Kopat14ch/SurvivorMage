using System;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Training.Scripts
{
    internal static class Saver
    {
        private static bool s_isInitialized;
        private static TrainingData s_trainingData;
        private static event Action<bool> s_OnLoaded;

        public static void Init(Action<bool> onLoaded)
        {
            if (s_isInitialized == false)
            {
                s_OnLoaded = onLoaded;
                s_isInitialized = true;
                TryLoadTraining();
            }
            else
            {
                onLoaded?.Invoke(true);
            }
        }

        public static void EndTraining()
        {
            s_trainingData.IsTrained = true;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(s_trainingData));
        }
        
        private static void TryLoadTraining() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadTraining);

        private static void LoadTraining(string json)
        {
            s_trainingData = JsonUtility.FromJson<TrainingData>(json);
            
            s_OnLoaded?.Invoke(s_trainingData.IsTrained);
        }
    }
}