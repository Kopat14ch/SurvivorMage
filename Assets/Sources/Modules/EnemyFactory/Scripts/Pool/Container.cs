using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.Particles.Scripts;
using UnityEngine;

namespace Sources.Modules.EnemyFactory.Scripts.Pool
{
    internal class Container : MonoBehaviour
    {
        private EnemyUnit _prefab;
        private ParticleSpawner _particleSpawner;
        
        public EnemyType EnemyType { get; private set; }

        private List<EnemyUnit> _units;
        
        public void Init(EnemyType enemyType, EnemyUnit prefab, ParticleSpawner particleSpawner)
        {
            EnemyType = enemyType;
            _prefab = prefab;
            _particleSpawner = particleSpawner;
            _units = new List<EnemyUnit>();
        }

        public void AddUnit(EnemyUnit unit)
        {
            _units.Add(unit);
        }

        public List<EnemyUnit> GetUnits(int unitCount)
        {
            List<EnemyUnit> inactiveUnits = new List<EnemyUnit>();

            if (_units.Count < unitCount)
            {
                foreach (var unit in _units)
                {
                    if (unit.gameObject.activeSelf == false)
                        inactiveUnits.Add(unit);
                }

                int difference = unitCount - inactiveUnits.Count;

                for (int i = 0; i < difference; i++)
                {
                    EnemyUnit spawnedEnemy = Instantiate(_prefab, transform.position, Quaternion.identity,
                        gameObject.transform);
                    
                    spawnedEnemy.SetParticleSpawner(_particleSpawner);
                    _units.Add(spawnedEnemy);
                    inactiveUnits.Add(spawnedEnemy);
                }
            }
            else
            {
                for (int i = 0; i < unitCount; i++)
                {
                    if (_units[i].gameObject.activeSelf == false)
                        inactiveUnits.Add(_units[i]);
                }
            }
            
            return inactiveUnits;
        }
    }
}
