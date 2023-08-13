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

        private List<EnemyUnit> _currentUnits;
        private List<EnemyUnit> _allWaveUnits;

        public void OnEnable()
        {
            _pool.Init();
        }

        public List<EnemyUnit> SpawnEnemies(Dictionary<EnemyType, int> wave)
        {
            _currentUnits = new List<EnemyUnit>();
            _allWaveUnits = new List<EnemyUnit>();
            
            foreach (KeyValuePair<EnemyType, int> entry in wave)
            {
                _currentUnits = _pool.GetObjects(entry.Key, entry.Value);

                foreach (EnemyUnit unit in _currentUnits)
                {
                    _allWaveUnits.Add(unit);
                    unit.SetTarget(_playerPosition);
                    unit.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
                    unit.gameObject.SetActive(true);
                }
            }

            return _allWaveUnits;
        }
    }
}
