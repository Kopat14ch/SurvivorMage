using System.Collections.Generic;
using Sources.Modules.Common;
using Sources.Modules.Enemy;
using Sources.Modules.Player;
using Sources.Modules.Weapons.Common;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.SurvivorMage.Scripts
{
    internal class SurvivorMageRoot : MonoBehaviour
    {
        [SerializeField] private Mage _mage;
        [SerializeField] private Weapon _stick;
        [SerializeField] private List<EnemyUnit> _enemyUnits;
        
        private FindCloseEnemy _findCloseEnemy;
        private ProjectilesPool _projectilesPool;

        private void Awake()
        {
            _findCloseEnemy = GetComponent<FindCloseEnemy>();
            _projectilesPool = GetComponent<ProjectilesPool>();
            
            _findCloseEnemy.Init(_enemyUnits, _mage);
            _stick.Init(_projectilesPool, _findCloseEnemy);
        }
    }
}
