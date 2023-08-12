using System.Collections.Generic;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Pools
{
    public class ProjectilesPool : MonoBehaviour
    {
        [SerializeField] private List<Projectile> _projectiles;

        private List<Projectile> _projectilesObjects;

        private const int Capacity = 20;

        private void Awake()
        {
            _projectilesObjects = new List<Projectile>();
            
            foreach (var projectile in _projectiles)
            {
                for (int i = 0; i < Capacity; i++)
                {
                    Projectile projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
                    
                    _projectilesObjects.Add(projectileInstance);
                    projectileInstance.Disable();
                }
            }
        }

        public List<Projectile> TryGetProjectiles(Projectile projectile)
        {
            if (_projectiles.Contains(projectile) == false)
                return null;

            List<Projectile> tempProjectile = new List<Projectile>();
            int index = _projectiles.IndexOf(projectile) * Capacity;
            int lastIndex = index + Capacity - 1;

            for (int i = index; i < lastIndex; i++)
                tempProjectile.Add(_projectilesObjects[i]);

            return tempProjectile;
        }
    }
}
