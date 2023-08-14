using System.Collections.Generic;
using Sources.Modules.Enemy;
using UnityEngine;

namespace Sources.Modules.EnemyFactory.Pool
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private List<EnemyUnit> _prefabs;
        [SerializeField] private int _startCapacity;
        [SerializeField] private Container _prefabContainer;

        private List<Container> _containers;

        public void Init()
        {
            _containers = new List<Container>();
            int enemyTypeIndex = 0;

            foreach (EnemyUnit prefab in _prefabs)
            {
                Container container = Instantiate(_prefabContainer, transform.position, Quaternion.identity,
                    transform);
                container.Init((EnemyType) enemyTypeIndex, prefab);
                _containers.Add(container);
                
                for (int i = 0; i < _startCapacity; i++)
                {
                    EnemyUnit spawned = Instantiate(prefab, transform.position, Quaternion.identity, container.transform);
                    spawned.gameObject.SetActive(false);
                    container.AddUnit(spawned);
                }
                
                if ((EnemyType) enemyTypeIndex == EnemyType.Other3)
                    break;
                
                enemyTypeIndex++;
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
