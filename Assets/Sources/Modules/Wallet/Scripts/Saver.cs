using System;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Wallet.Scripts
{
    internal static class Saver
    {
        private static bool s_isInitialized;
        private static WalletData s_walletData;
        private static event Action s_OnLoaded;

        public static void Init(Action onLoaded)
        {
            if (s_isInitialized == false)
            {
                s_OnLoaded = onLoaded;
                s_isInitialized = true;
                TryLoadWallet();
            }
            else
            {
                onLoaded?.Invoke();
            }
        }

        public static void SaveWallet(int coins)
        {
            s_walletData.Coins = coins;
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(s_walletData));
        }

        public static WalletData GetWallet()
        {
#if UNITY_EDITOR
            return null;
#endif
            
            return s_walletData;
        }

        private static void TryLoadWallet() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadWallet);

        private static void LoadWallet(string json)
        {
            s_walletData = JsonUtility.FromJson<WalletData>(json);
            s_OnLoaded?.Invoke();
        }
    }
}