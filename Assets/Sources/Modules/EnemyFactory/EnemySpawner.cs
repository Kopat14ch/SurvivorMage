using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory.Pool;
using UnityEngine;

namespace Sources.Modules.EnemyFactory
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private EnemyPool _pool;
        [SerializeField] private Transform _playerPosition;
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private int _enemyCount;

        private List<EnemyUnit> _currentUnits;

        private void OnEnable()
        {
            _pool.Init();
            _currentUnits = _pool.GetObjects(_enemyType, _enemyCount);
            SpawnEnemies();
        }

        public void SpawnEnemies()
        {
            foreach (EnemyUnit unit in _currentUnits)
            {
                unit.SetTarget(_playerPosition);
                unit.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
                unit.gameObject.SetActive(true);
            }
        }
        
        public List<EnemyUnit> GetEnemies()
        {
            return _currentUnits;
        }
    }
}
