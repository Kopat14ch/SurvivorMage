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
                gameObject.SetActive(false);
                Taken?.Invoke();
            }
        }
    }
}
