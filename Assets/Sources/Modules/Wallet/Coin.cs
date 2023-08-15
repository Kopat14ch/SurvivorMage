using System;
using Sources.Modules.Player;
using UnityEngine;

namespace Sources.Modules.Wallet
{
    public class Coin : MonoBehaviour
    {
        public event Action Taken;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Mage _))
            {
                Taken?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
}
