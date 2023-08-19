using System;
using Sources.Modules.Weapons.Scripts.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Workshop.Scripts.UI
{
    public class SpellSlot : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private TMP_Text _equippedText;
        [SerializeField] private Color _equippedColor;
        [SerializeField] private Color _unequippedColor;
        [SerializeField] private bool _isBought = false;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _equipButton;
        [SerializeField] private Projectile _spellProjectile;
        [SerializeField] private SpellCaster _spellCaster;

        public event Action<int, SpellSlot> BuyButtonPressed;
        public event Action EquipButtonPressed;

        public bool IsBought => _isBought;
        public bool IsEquipped { get; private set; }
        
        private void Awake()
        {
            _priceText.text = _price.ToString();
            _damageText.text = _spellProjectile.BaseDamage.ToString();
            
            TryHideBuyButton();
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(OnBuyButtonPressed);
            _equipButton.onClick.AddListener(OnEquipButtonPressed);
        }
        
        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(OnBuyButtonPressed);
            _equipButton.onClick.RemoveListener(OnEquipButtonPressed);
        }

        public void BuySpell()
        {
            _isBought = true;
            TryHideBuyButton();
        }
        
        private void OnBuyButtonPressed() => BuyButtonPressed?.Invoke(_price, this);

        private void OnEquipButtonPressed() => EquipButtonPressed?.Invoke();
        
        private void TryHideBuyButton()
        {
            CanvasGroup buyButtonCanvas = _buyButton.GetComponent<CanvasGroup>();
            CanvasGroup equipButtonCanvas = _equipButton.GetComponent<CanvasGroup>();

            if (_isBought)
            {
                HideCanvas(buyButtonCanvas);
                ShowCanvas(equipButtonCanvas);
            }
            else
            {
                HideCanvas(equipButtonCanvas);
                ShowCanvas(buyButtonCanvas);
            }
        }

        private void HideCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        private void ShowCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
    }
}
