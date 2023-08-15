using Sources.Modules.Finder;
using Sources.Modules.Weapons.Common;
using Sources.Modules.Weapons.Pools;
using UnityEngine;

namespace Sources.Modules.Weapons.Base
{
    internal class Stick : Weapon
    {
        [SerializeField] private Projectile _projectile;

        public override void Init(ProjectilesPool projectilesPool, FinderCloseEnemy finderCloseEnemy)
        {
            Projectiles = projectilesPool.TryGetObjects(_projectile);
            FinderCloseEnemy = finderCloseEnemy;
            
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
