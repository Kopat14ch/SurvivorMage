using System.Collections;
using System.Collections.Generic;
using Sources.Modules.Common;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.Modules.Weapons.Common
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField, Range(MinCooldown, MaxCooldown)] private float _cooldown;
        
        protected FindCloseEnemy FindCloseEnemy;
        protected List<Projectile> Projectiles;
        protected Coroutine ShootingWork;
        protected bool StopShooting;
        protected float CurrentCooldown;
        
        private const int MinCooldown = 1;
        private const int MaxCooldown = 60;

        protected bool CanShoot => CurrentCooldown <= 0;
        protected ShootPoint ShootPoint => _shootPoint;
        protected float Cooldown => _cooldown;

        public abstract void Init(ProjectilesPool projectilesPool, FindCloseEnemy findCloseEnemy);

        protected abstract void StartShooting();
        
        protected IEnumerator Shooting()
        {
            int indexShoot = 0;
            
            while (StopShooting == false)
            {
                if (CanShoot == false)
                {
                    CurrentCooldown -= Time.deltaTime;
                }
                else
                {
                    Projectiles[indexShoot].Launch(ShootPoint, FindCloseEnemy.GetCloseEnemyPosition());
                    CurrentCooldown = Cooldown;
                    indexShoot++;
                    indexShoot %= Projectiles.Count;
                }

                yield return null;
            }
        }
    }
}
