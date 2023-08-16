using TMPro;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text InGameText;
        [SerializeField] private TMP_Text WorkshopText;
        
        public void ChangeCoinText(int coin)
        {
            InGameText.text = coin.ToString();
            WorkshopText.text = coin.ToString();
        }
    }
}
