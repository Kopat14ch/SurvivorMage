using System.Collections;
using System.Collections.Generic;
using Sources.Modules.Finder;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.Modules.Weapons.Common
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField, Range(MinCooldown, MaxCooldown)] private float _cooldown;

        private bool _stopShooting;
        protected FindCloseEnemy FindCloseEnemy;
        protected List<Projectile> Projectiles;
        protected Coroutine ShootingWork;

        private const int MinCooldown = 1;
        private const int MaxCooldown = 60;
        
        private float _currentCooldown;

        private bool CanShoot => _currentCooldown <= 0;
        private float Cooldown => _cooldown;

        public abstract void Init(ProjectilesPool projectilesPool, FindCloseEnemy findCloseEnemy);

        protected abstract void StartShooting();

        protected IEnumerator Shooting()
        {
            int indexShoot = 0;
            
            while (_stopShooting == false)
            {
                if (CanShoot == false)
                {
                    _currentCooldown -= Time.deltaTime;
                }
                else
                {
                    Projectiles[indexShoot].Launch(_shootPoint, FindCloseEnemy.GetCloseEnemyPosition());
                    _currentCooldown = Cooldown;
                    indexShoot++;
                    indexShoot %= Projectiles.Count;
                }

                yield return null;
            }
        }
    }
}
