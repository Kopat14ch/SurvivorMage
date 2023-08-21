using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts
{
    internal static class Saver
    {
        private const string Spells = nameof(Spells);

        public static void SaveSpells(List<SpellSlotData> spellCasters)
        {
            PlayerPrefs.DeleteAll();
            BinaryFormatter formatter = new();
            MemoryStream memoryStream = new();

            formatter.Serialize(memoryStream, spellCasters);
            string data = Convert.ToBase64String(memoryStream.ToArray());

            PlayerPrefs.SetString(Spells, data);
            PlayerPrefs.Save();
        }
        
        public static List<SpellSlotData> LoadSpells()
        {
            if (PlayerPrefs.HasKey(Spells))
            {
                string data = PlayerPrefs.GetString(Spells);
                byte[] bytes = Convert.FromBase64String(data);

                BinaryFormatter formatter = new ();
                MemoryStream memoryStream = new (bytes);

                return formatter.Deserialize(memoryStream) as List<SpellSlotData>;
            }


            return null;
        }
    }
}