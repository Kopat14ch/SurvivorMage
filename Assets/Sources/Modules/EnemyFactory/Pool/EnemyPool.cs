using System.Collections.Generic;
using Sources.Modules.Enemy;
using UnityEngine;

namespace Sources.Modules.EnemyFactory.Pool
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private List<EnemyUnit> _prefabs;
        [SerializeField] private List<Container> _containers;
        [SerializeField] private int _startCapacity;
        
        
        public void Init()
        {
            foreach (EnemyUnit prefab in _prefabs)
            {
                foreach (Container container in _containers)
                {
                    if (prefab.EnemyType == container.EnemyType)
                    {
                        for (int i = 0; i < _startCapacity; i++)
                        {
                            EnemyUnit spawned = Instantiate(prefab, transform.position, Quaternion.identity,
                                container.transform);
                            spawned.gameObject.SetActive(false);
                            container.AddUnit(spawned);
                        }
                    }
                }
            }
        }

        public List<EnemyUnit> GetObjects(EnemyType enemyType, int unitCount)
        {
            List<EnemyUnit> units = new List<EnemyUnit>();

            foreach (Container container in _containers)
            {
                if (container.EnemyType == enemyType)
                {
                    units = container.GetUnits(unitCount);
                    break;
                }
            }

            return units;
        }
    }
}
