using Sources.Modules.EnemyFactory;
using Sources.Modules.EnemyFactory.Pool;
using Sources.Modules.Finder;
using Sources.Modules.Player;
using Sources.Modules.Player.MVP;
using Sources.Modules.Weapons.Base;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.SurvivorMage.Scripts
{
    [RequireComponent(typeof(FinderCloseEnemy))]
    internal class SurvivorMageRoot : MonoBehaviour
    {
        [SerializeField] private Mage _mage;
        [SerializeField] private Staff _staff;
        [SerializeField] private ProjectilesPool _projectilesPool;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private PlayerSetup _setup;

        private FinderCloseEnemy _finderCloseEnemy;

        private void Awake()
        {
            _finderCloseEnemy = GetComponent<FinderCloseEnemy>();

            _setup.Init(_mage);
            _finderCloseEnemy.Init(_mage);
            _enemyPool.Init();
            _enemySpawner.Init(_enemyPool);
            _projectilesPool.Init();
            _staff.Init(_finderCloseEnemy, _projectilesPool);
        }
    }
}
