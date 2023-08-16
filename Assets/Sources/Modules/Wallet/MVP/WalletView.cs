using TMPro;
using UnityEngine;

namespace Sources.Modules.Wallet.MVP
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _inGameText;
        [SerializeField] private TMP_Text _workshopText;
        
        public void ChangeCoinText(int coin)
        {
            _inGameText.text = coin.ToString();
            _workshopText.text = coin.ToString();
        }
    }
}
