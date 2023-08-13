using System.Collections.Generic;
using System.Linq;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory.Pool;
using UnityEngine;

namespace Sources.Modules.EnemyFactory
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private Transform _playerPosition;

        private EnemyPool _enemyPool;
        private List<EnemyUnit> _currentUnits;
        private List<EnemyUnit> _allWaveUnits;

        public void Init(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        public List<EnemyUnit> SpawnEnemies(Dictionary<List<EnemyType>, int> wave)
        {
            _currentUnits = new List<EnemyUnit>();
            _allWaveUnits = new List<EnemyUnit>();

            int enemyTypeIndex = 0;

            for (int i = 0; i < wave.Count; i++, enemyTypeIndex++)
            {
                enemyTypeIndex %= wave.Keys.ElementAt(i).Count;
                
                Debug.Log(wave.Keys.ElementAt(i)[enemyTypeIndex]);

                _currentUnits = _enemyPool.GetObjects(wave.Keys.ElementAt(i)[enemyTypeIndex], wave.Values.ElementAt(i));
                
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
