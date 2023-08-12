using Sources.Modules.Finder;
using Sources.Modules.Weapons.Common;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.Modules.Weapons.Base
{
    internal class Stick : Weapon
    {
        [SerializeField] private Projectile _projectile;

        public override void Init(ProjectilesPool projectilesPool, FindCloseEnemy findCloseEnemy)
        {
            Projectiles = projectilesPool.TryGetProjectiles(_projectile);
            FindCloseEnemy = findCloseEnemy;
            
            StartShooting();
        }

        protected override void StartShooting()
        {
            if (ShootingWork != null)
                StopCoroutine(ShootingWork);

            ShootingWork = StartCoroutine(Shooting());
        }
    }
}
