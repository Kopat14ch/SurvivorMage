using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Agava.YandexGames;
using UnityEngine;
using PlayerPrefs = UnityEngine.PlayerPrefs;

namespace Sources.Modules.Workshop.Scripts
{
    internal static class Saver
    {
        private const string Spells = nameof(Spells);
        private static List<SpellSlotData> _spellSlotData;

        public static void Init()
        {
            TryLoadSpells();
        }

        public static void SaveSpells(List<SpellSlotData> spellCasters)
        {
#if UNITY_EDITOR
            return;
#endif
            
            PlayerAccount.SetCloudSaveData(JsonUtility.ToJson(spellCasters));
        }

        private static void TryLoadSpells() => PlayerAccount.GetCloudSaveData(onSuccessCallback: LoadSpells);

        private static void LoadSpells(string jsonLoaded)
        {
            _spellSlotData = JsonUtility.FromJson<List<SpellSlotData>>(jsonLoaded);
        }

        public static List<SpellSlotData> GetSpells() => _spellSlotData.GetRange(0, _spellSlotData.Count);
    }
}