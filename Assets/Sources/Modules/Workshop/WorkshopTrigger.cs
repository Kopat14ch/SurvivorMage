using System;
using UnityEngine;
using Sources.Modules.Player;

namespace Sources.Modules.Workshop
{
    public class WorkshopTrigger : MonoBehaviour
    {
        public event Action PlayerEntered;
        public event Action PlayerCameOut;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Mage>(out _))
                PlayerEntered?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Mage>(out _))
                PlayerCameOut?.Invoke();
        }
    }
}
