using Sources.Modules.EnemyFactory.Scripts;
using Sources.Modules.EnemyFactory.Scripts.Pool;
using Sources.Modules.Finder.Scripts;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Player.Scripts.MVP;
using Sources.Modules.Wallet.Scripts.MVP;
using Sources.Modules.Weapons.Base;
using Sources.Modules.Weapons.Scripts;
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
        [SerializeField] private PlayerSetup _playerSetup;
        [SerializeField] private WalletSetup _walletSetup;
        [SerializeField] private PlayerView _playerView;

        private FinderCloseEnemy _finderCloseEnemy;

        private void Awake()
        {
            _finderCloseEnemy = GetComponent<FinderCloseEnemy>();

            _playerSetup.Init(_mage);
            _finderCloseEnemy.Init(_mage);
            _enemyPool.Init();
            _enemySpawner.Init(_enemyPool);
            _projectilesPool.Init();
            _staff.Init(_finderCloseEnemy, _projectilesPool);
            _walletSetup.Init(_playerView);
        }
    }
}
