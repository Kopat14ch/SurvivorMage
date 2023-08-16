using System;
using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.Wallet.Pool;
using UnityEngine;

namespace Sources.Modules.Wallet
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private CoinPool _pool;

        public event Action<Coin> CoinSpawned;
        
        private List<EnemyUnit> _enemies;

        private void OnDisable()
        {
            foreach (EnemyUnit enemy in _enemies)
                enemy.Died -= SpawnCoin;
        }

        public void SetEnemies(List<EnemyUnit> enemies)
        {
            _enemies = enemies;

            foreach (EnemyUnit enemy in _enemies)
                enemy.Died += SpawnCoin;
        }

        private void SpawnCoin(EnemyUnit enemy)
        {
            Coin coin = _pool.GetCoin();
            coin.transform.position = enemy.transform.position;
            coin.gameObject.SetActive(true);
            
            CoinSpawned?.Invoke(coin);
            enemy.Died -= SpawnCoin;
        }
    }
}
