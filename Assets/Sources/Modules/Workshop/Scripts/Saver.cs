using System;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts
{
    internal static class Saver
    {
        private const string Spells = nameof(Spells);
        private static List<SpellSlotData> s_spellSlotData;
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

        public static void SaveSpells(List<SpellSlotData> spellCasters)
        {
#if UNITY_EDITOR
            return;
#endif
            Debug.Log($"SAVED: {JsonUtility.ToJson(spellCasters)}");
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(spellCasters));
        }

        private static void TryLoadSpells() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadSpells);

        private static void LoadSpells(string jsonLoaded)
        {
            s_spellSlotData = JsonUtility.FromJson<List<SpellSlotData>>(jsonLoaded);
            s_OnLoad?.Invoke();
        }

        public static List<SpellSlotData> GetSpells()
        {
#if UNITY_EDITOR
            return null;
#endif
            foreach (var spellSlotData in s_spellSlotData)
            {
                Debug.Log($"LOAD: {spellSlotData.SpellType}");
            }
            return s_spellSlotData.GetRange(0, s_spellSlotData.Count);
        }
    }
}