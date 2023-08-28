using System;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    internal static class Saver
    {
        private static bool s_isInitialized;
        private static event Action s_OnLoaded;
        private static PlayerData s_playerData;

        public static void Init(Action onLoaded)
        {
            if (s_isInitialized)
            {
                s_OnLoaded = onLoaded;
                s_isInitialized = true;
            }
            else
            {
                onLoaded?.Invoke();
            }
        }

        public static PlayerData GetPlayerData()
        {
#if UNITY_EDITOR
            return null;
#endif

            return s_playerData;
        }

        public static void SaveData(PlayerData playerData)
        {
            s_playerData = playerData;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(s_playerData));
        }
        
        private static void TryLoadPlayer() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadPlayer);

        private static void LoadPlayer(string json)
        {
            s_playerData = JsonUtility.FromJson<PlayerData>(json);
            s_OnLoaded?.Invoke();
        }
    }
}