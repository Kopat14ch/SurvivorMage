using Sources.Modules.EnemyFactory;
using Sources.Modules.EnemyFactory.Pool;
using Sources.Modules.Finder;
using Sources.Modules.Player;
using Sources.Modules.Weapons.Common;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.SurvivorMage.Scripts
{
    [RequireComponent(typeof(FindCloseEnemy),
        typeof(ProjectilesPool))]
    internal class SurvivorMageRoot : MonoBehaviour
    {
        [SerializeField] private Mage _mage;
        [SerializeField] private Weapon _stick;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemySpawner _enemySpawner;

        private FindCloseEnemy _findCloseEnemy;
        private ProjectilesPool _projectilesPool;

        private void Awake()
        {
            _findCloseEnemy = GetComponent<FindCloseEnemy>();
            _projectilesPool = GetComponent<ProjectilesPool>();
            
            _projectilesPool.Init();
            _findCloseEnemy.Init(_mage);
            _stick.Init(_projectilesPool, _findCloseEnemy);
            _enemyPool.Init();
            _enemySpawner.Init(_enemyPool);
        }
    }
}
