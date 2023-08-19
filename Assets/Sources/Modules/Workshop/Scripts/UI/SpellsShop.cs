using System;
using System.Collections.Generic;
using Sources.Modules.Weapons.Scripts;
using Sources.Modules.Weapons.Scripts.Base;
using TMPro;
using UnityEngine;

namespace Sources.Modules.Workshop.Scripts.UI
{
    public class SpellsShop : MonoBehaviour
    {
        [SerializeField] private List<SpellSlot> _spellSlots;
        [SerializeField] private int _activeSpellsLimit;
        [SerializeField] private TMP_Text _activeSpellsText;
        [SerializeField] private Color _activeSpellsAvailableColor;
        [SerializeField] private Color _activeSpellsEnoughColor;

        private Staff _staff;

        public event Action<int, SpellSlot> SlotBuyButtonPressed;

        public void Init(Staff staff)
        {
            _staff = staff;

            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed += OnSlotBuyButtonPressed;
                slot.EquipButtonPressed += OnEquipButtonPressed;
                
                if (slot.IsEquipped)
                    _staff.AddSpellCaster(slot.SpellType);
            }
            
            CheckSpellsLimit();
        }

        private void OnDisable()
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed -= OnSlotBuyButtonPressed;
                slot.EquipButtonPressed -= OnEquipButtonPressed;
            }
        }

        private void OnEquipButtonPressed(SpellType spellType, SpellSlot spellSlot)
        {
            if (spellSlot.IsEquipped == false)
                EquipSpell(spellType, spellSlot);
            else
                UnequipSpell(spellType, spellSlot);
        }

        private void EquipSpell(SpellType spellType, SpellSlot spellSlot)
        {
            _staff.AddSpellCaster(spellType);
            spellSlot.EquipSpell();
            CheckSpellsLimit();
        }

        private void UnequipSpell(SpellType spellType, SpellSlot spellSlot)
        {
            _staff.RemoveSpellCaster(spellType);
            spellSlot.UnequipSpell();
            CheckSpellsLimit();
        }
        
        public void BuySpell(SpellSlot spellSlot)
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                if (slot == spellSlot)
                    spellSlot.BuySpell();
            }
        }

        private void CheckSpellsLimit()
        {
            if (_staff.ActiveSpells < _activeSpellsLimit)
            {
                foreach (SpellSlot slot in _spellSlots)
                    slot.EnableEquipButton();

                _activeSpellsText.color = _activeSpellsAvailableColor;
            }
            else
            {
                foreach (SpellSlot slot in _spellSlots)
                {
                    if (slot.IsEquipped)
                        slot.EnableEquipButton();
                    else
                        slot.DisableEquipButton();

                    _activeSpellsText.color = _activeSpellsEnoughColor;
                }
            }
            
            RewriteActiveSpellsLimit(_staff.ActiveSpells);
        }

        private void RewriteActiveSpellsLimit(int currentActiveSpells)
        {
            _activeSpellsText.text = (currentActiveSpells + "/" + _activeSpellsLimit);
        }
        
        private void OnSlotBuyButtonPressed(int price, SpellSlot slot) => SlotBuyButtonPressed?.Invoke(price, slot);
    }
}
