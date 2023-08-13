using System.Collections.Generic;
using Sources.Modules.Enemy;
using UnityEngine;

namespace Sources.Modules.EnemyFactory.Pool
{
    public class Container : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType = EnemyType.Demon1;
        [SerializeField] private GameObject _prefab;

        public EnemyType EnemyType => _enemyType;

        private List<EnemyUnit> _units = new List<EnemyUnit>();

        public void AddUnit(EnemyUnit unit)
        {
            _units.Add(unit);
        }

        public List<EnemyUnit> GetUnits(int desiredCount)
        {
            List<EnemyUnit> inactiveUnits = new List<EnemyUnit>();

            if (_units.Count < desiredCount)
            {
                foreach (var unit in _units)
                {
                    if (unit.gameObject.activeSelf == false)
                        inactiveUnits.Add(unit);
                }

                int difference = desiredCount - inactiveUnits.Count;

                for (int i = 0; i < difference; i++)
                {
                    GameObject spawned = Instantiate(_prefab, transform.position, Quaternion.identity,
                        gameObject.transform);
                    //spawned.gameObject.SetActive(false);
                    EnemyUnit enemy = spawned.GetComponent<EnemyUnit>();
                    _units.Add(enemy);
                    inactiveUnits.Add(enemy);
                }
            }
            else
            {
                for (int i = 0; i < desiredCount; i++)
                {
                    if (_units[i].gameObject.activeSelf == false)
                        inactiveUnits.Add(_units[i]);
                }
            }
            
            return inactiveUnits;
        }
    }
}
