using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Common
{
    public class UpgradePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private TMP_Text _upgradeValue;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private int _price;
        [SerializeField] private string _maxText;

        public Button BuyButton => _buyButton;
        public int Price => _price;

        private void Awake()
        {
            ChangePriceText(_price.ToString());
        }

        public void ChangeCurrentValueText(string text)
        {
            _currentValue.text = text;
        }

        public void ChangeUpgradeValueText(string text)
        {
            _upgradeValue.text = text;
        }

        public void MaximizeValue()
        {
            ChangeUpgradeValueText(_maxText);
            ChangePriceText(_maxText);
            _buyButton.enabled = false;
        }
        
        public void ChangePriceText(string text)
        {
            _priceText.text = text;
        }
    }
}
