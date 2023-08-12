using System.Collections.Generic;
using Sources.Modules.Pools;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Pools
{
    public class ProjectilesPool : Pool<Projectile>
    {
        private List<Projectile> _projectilesObjects;

        private const int Capacity = 20;

        public override void Init()
        {
            _projectilesObjects = new List<Projectile>();
            
            foreach (var projectile in _gameObjects)
            {
                for (int i = 0; i < Capacity; i++)
                {
                    Projectile projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                    
                    _projectilesObjects.Add(projectileInstance);
                    projectileInstance.Disable();
                }
            }
        }

        public override List<Projectile> TryGetObjects(Projectile projectile)
        {
            if (_gameObjects.Contains(projectile) == false)
                return null;

            List<Projectile> tempProjectile = new List<Projectile>();
            int index = _gameObjects.IndexOf(projectile) * Capacity;
            int lastIndex = index + Capacity - 1;

            for (int i = index; i < lastIndex; i++)
                tempProjectile.Add(_projectilesObjects[i]);
            
            return tempProjectile;
        }
    }
}
