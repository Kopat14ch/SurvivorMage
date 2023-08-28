using System;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts
{
    internal static class Saver
    {
        private const string Spells = nameof(Spells);
        private static SpellSlotDates s_spellSlotData;
        private static event Action s_OnLoad;
        private static bool s_isInitialize;

        public static void Init(Action onLoad)
        {
            if (s_isInitialize == false)
            {
                s_isInitialize = true;
                s_OnLoad = onLoad;
                TryLoadSpells();
            }
        }

        public static void SaveSpells(SpellSlotDates spellCasters)
        {
#if UNITY_EDITOR
            return;
#endif
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(spellCasters));
        }

        private static void TryLoadSpells() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadSpells);

        private static void LoadSpells(string jsonLoaded)
        {
            s_spellSlotData = JsonUtility.FromJson<SpellSlotDates>(jsonLoaded);
            s_OnLoad?.Invoke();
        }

        public static SpellSlotDates GetSpells()
        {
#if UNITY_EDITOR
            return null;
#endif
            
            return s_spellSlotData;
        }
    }
}