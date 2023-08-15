using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Wallet.Pool
{
    public class CoinPool : MonoBehaviour
    {
        [SerializeField] private Coin _prefab;
        [SerializeField] private int _startCapacity;

        private List<Coin> _coins = new List<Coin>();

        private void Awake()
        {
            for (int i = 0; i < _startCapacity; i++)
            {
                Coin spawned = InitCoin();
                spawned.gameObject.SetActive(false);
                _coins.Add(spawned);
            }
        }

        public Coin GetCoin()
        {
            Coin inactiveCoin = null;

            foreach (Coin coin in _coins)
            {
                if (coin.gameObject.activeSelf == false)
                    inactiveCoin = coin;
            }

            if (inactiveCoin == null)
            {
                Coin spawned = InitCoin();
                _coins.Add(spawned);
                inactiveCoin = spawned;
            }

            return inactiveCoin;
        }

        private Coin InitCoin()
        {
            Coin spawned = Instantiate(_prefab, transform.position, Quaternion.identity,
                transform);
            return spawned;
        }
    }
}
