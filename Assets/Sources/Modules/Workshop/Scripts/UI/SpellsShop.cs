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
        private SpellSlotDates _slotDates;

        public event Action<int, SpellSlot> SlotBuyButtonPressed;

        public void Init(Staff staff)
        {
            _staff = staff;

#if UNITY_EDITOR
            InitSaved();
            return;
#endif
            Saver.Init(InitSaved);
        }

        private void InitSaved()
        {
            _slotDates = Saver.GetSpells() ?? new SpellSlotDates()
            {
                SlotDates = new List<SpellType>(),
                ActiveSpells = new List<SpellType>()
            };

            foreach (SpellSlot slot in _spellSlots)
            {
                slot.BuyButtonPressed += OnSlotBuyButtonPressed;
                slot.EquipButtonPressed += OnEquipButtonPressed;
                
                foreach (var slotData in _slotDates.SlotDates)
                {
                    if (slotData == slot.SpellType)
                    {
                        slot.BuySpell();

                        if (_slotDates.ActiveSpells.Contains(slotData))
                        {
                            slot.EquipSpell();
                        }
                    }
                }

                if (slot.IsEquipped)
                {
                    AddActiveSpell(slot.SpellType);
                    _staff.AddSpellCaster(slot.SpellType);
                }
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
                UnEquipSpell(spellType, spellSlot);
        }

        private void EquipSpell(SpellType spellType, SpellSlot spellSlot)
        {
            AddActiveSpell(spellType);
            _staff.AddSpellCaster(spellType);
            spellSlot.EquipSpell();
            CheckSpellsLimit();
        }

        private void UnEquipSpell(SpellType spellType, SpellSlot spellSlot)
        {
            RemoveActiveSpell(spellType);
            _staff.RemoveSpellCaster(spellType);
            spellSlot.UnequipSpell();
            CheckSpellsLimit();
        }
        
        public void BuySpell(SpellSlot spellSlot)
        {
            foreach (SpellSlot slot in _spellSlots)
            {
                if (slot == spellSlot)
                {
                    spellSlot.BuySpell();

                    _slotDates.SlotDates.Add(spellSlot.SpellType);
                    
                    Saver.SaveSpells(_slotDates);
                }
            }
        }

        private void CheckSpellsLimit()
        {
            if (_staff.ActiveSpellsCount < _activeSpellsLimit)
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
            
            RewriteActiveSpellsLimit(_staff.ActiveSpellsCount);
        }

        private void RewriteActiveSpellsLimit(int currentActiveSpells)
        {
            _activeSpellsText.text = (currentActiveSpells + "/" + _activeSpellsLimit);
        }

        private void AddActiveSpell(SpellType spell)
        {
            _slotDates.ActiveSpells.Add(spell);
            Saver.SaveSpells(_slotDates);
        }

        private void RemoveActiveSpell(SpellType spell)
        {
            _slotDates.ActiveSpells.Remove(spell);
            Saver.SaveSpells(_slotDates);
        }

        private void OnSlotBuyButtonPressed(int price, SpellSlot slot) => SlotBuyButtonPressed?.Invoke(price, slot);
    }
}
