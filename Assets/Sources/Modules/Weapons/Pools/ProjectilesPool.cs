using System.Collections.Generic;
using Sources.Modules.Pools;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Pools
{
    public class ProjectilesPool : Pool<Projectile>
    {
        public override void Init()
        {
            GameObjectsInPool = new List<Projectile>();
            
            foreach (var projectile in GameObjects)
            {
                for (int i = 0; i < Capacity; i++)
                {
                    Projectile projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                    
                    GameObjectsInPool.Add(projectileInstance);
                    projectileInstance.Disable();
                }
            }
        }

        public override List<Projectile> TryGetObjects(Projectile projectile)
        {
            if (GameObjects.Contains(projectile) == false)
                return null;

            List<Projectile> tempProjectile = new List<Projectile>();
            int index = GameObjects.IndexOf(projectile) * Capacity;
            int lastIndex = index + Capacity - 1;

            for (int i = index; i < lastIndex; i++)
                tempProjectile.Add(GameObjectsInPool[i]);
            
            return tempProjectile;
        }
    }
}
