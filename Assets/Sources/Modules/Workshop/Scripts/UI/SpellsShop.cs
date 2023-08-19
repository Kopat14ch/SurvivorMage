using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts.UI
{
    public class SpellsShop : MonoBehaviour
    {
        [SerializeField] private List<SpellSlot> _spellSlots;

        public event Action<int, SpellSlot> SlotBuyButtonPressed;

        private void Awake()
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed += OnSlotBuyButtonPressed;
                //slot.EquipButtonPressed +=
            }
        }

        private void OnDisable()
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed -= OnSlotBuyButtonPressed;
                //slot.EquipButtonPressed -=
            }
        }

        public void BuySpell(SpellSlot spellSlot)
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                if (slot == spellSlot)
                    spellSlot.BuySpell();
            }
        }
        
        private void OnSlotBuyButtonPressed(int price, SpellSlot slot) => SlotBuyButtonPressed?.Invoke(price, slot);
    }
}
