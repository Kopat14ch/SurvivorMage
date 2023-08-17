using System.Collections;
using Sources.Modules.Finder.Scripts;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Common
{
    internal class SpellCaster : MonoBehaviour
    {
        [SerializeField, Range(MinCooldown, MaxCooldown)] private float _cooldown;
        [SerializeField] private SpellType _spellType;
        
        private ShootPoint _shootPoint;
        private ProjectilesPool _pool;
        private bool _stopShooting;

        private FinderCloseEnemy _finderCloseEnemy;
        private Coroutine _shootingWork;

        private const int MinCooldown = 1;
        private const int MaxCooldown = 60;
        
        private float _currentCooldown;

        private bool CanShoot => _currentCooldown <= 0;
        private float Cooldown => _cooldown;

        public void Init(ShootPoint shootPoint ,FinderCloseEnemy finderCloseEnemy, ProjectilesPool pool)
        {
            _shootPoint = shootPoint;
            _finderCloseEnemy = finderCloseEnemy;
            _pool = pool;
            //_stopShooting = true;
        }

        public void StartCasting()
        {
            if (_shootingWork != null)
                StopCoroutine(_shootingWork);

            _shootingWork = StartCoroutine(Shooting());
        }

        private IEnumerator Shooting()
        {
            while (_stopShooting == false)
            {
                if (CanShoot == false)
                {
                    _currentCooldown -= Time.deltaTime;
                }
                else
                {
                    Projectile projectile = _pool.GetObject(_spellType);
                    projectile.Launch(_shootPoint, _finderCloseEnemy.GetCloseEnemyPosition());
                    _currentCooldown = Cooldown;
                }

                yield return null;
            }
        }
    }
}
