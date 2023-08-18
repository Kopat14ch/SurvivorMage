using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.Modules.Common;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory.Scripts.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.EnemyFactory.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private Transform _playerPosition;

        private const float ObstacleCheckRadius = 2f;
        private const float SpawningCooldown = 0.5f;

        private Collider2D[] _collidersBuffer = new Collider2D[2];
        private int _collidersCount;
        private EnemyPool _enemyPool;
        private List<EnemyUnit> _currentUnits;
        private List<EnemyUnit> _allWaveUnits;
        private Coroutine _spawningWork;

        public void Init(EnemyPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        public void SpawnEnemies(Dictionary<List<EnemyType>, int> wave)
        {
            _currentUnits = new List<EnemyUnit>();
            _allWaveUnits = new List<EnemyUnit>();

            int enemyTypeIndex = 0;

            for (int i = 0; i < wave.Count; i++, enemyTypeIndex++)
            {
                enemyTypeIndex %= wave.Keys.ElementAt(i).Count;

                _currentUnits = _enemyPool.GetObjects(wave.Keys.ElementAt(i)[enemyTypeIndex], wave.Values.ElementAt(i));
                
                foreach (EnemyUnit unit in _currentUnits)
                {
                    if (_allWaveUnits.Contains(unit))
                        continue;
                    
                    _allWaveUnits.Add(unit);
                    unit.SetTarget(_playerPosition);
                }
            }

            if (_spawningWork != null)
                StopCoroutine(_spawningWork);

            _spawningWork = StartCoroutine(Spawning());
        }

        public List<EnemyUnit> GetEnemies() => _allWaveUnits.GetRange(0, _allWaveUnits.Count);

        private IEnumerator Spawning()
        {
            WaitForSeconds waitForSeconds = new (SpawningCooldown);

            foreach (var enemyUnit in _allWaveUnits)
            {
                if (enemyUnit.IsDie)
                    continue;

                enemyUnit.transform.position = _spawnPoints[5].transform.position;
                
                _collidersCount = Physics2D.OverlapCircleNonAlloc(enemyUnit.transform.position, ObstacleCheckRadius, _collidersBuffer);

                for (int i = 0; i < _collidersCount; i++)
                {
                    bool inObstacle = _collidersBuffer[i] != enemyUnit.Collider2D && _collidersBuffer[i].TryGetComponent(out Obstacle _);
                    
                    if (inObstacle)
                    {
                        while (inObstacle)
                        {
                            enemyUnit.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;

                            _collidersCount = Physics2D.OverlapCircleNonAlloc(enemyUnit.transform.position, ObstacleCheckRadius, _collidersBuffer);
                            inObstacle = _collidersBuffer[i] != enemyUnit.Collider2D && _collidersBuffer[i].TryGetComponent(out Obstacle _);
                            
                            yield return waitForSeconds;
                        }
                        break;
                    }
                }
                
                enemyUnit.gameObject.SetActive(true);

                yield return waitForSeconds;
            }
            
        }
    }
}
