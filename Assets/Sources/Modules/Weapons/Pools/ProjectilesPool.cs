using System.Collections.Generic;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Pools
{
    public class ProjectilesPool : MonoBehaviour
    {
        [SerializeField] private List<Projectile> _prefabs;
        [SerializeField] private int _startCapacity;
        [SerializeField] private ProjectileContainer _prefabContainer;

        private List<ProjectileContainer> _containers;

        public void Init()
        {
            _containers = new List<ProjectileContainer>();

            foreach (Projectile prefab in _prefabs)
            {
                ProjectileContainer container = Instantiate(_prefabContainer, transform.position, Quaternion.identity,
                    transform);
                container.Init(prefab.SpellType, prefab);
                _containers.Add(container);
                
                for (int i = 0; i < _startCapacity; i++)
                {
                    Projectile spawned = Instantiate(prefab, transform.position, Quaternion.identity, container.transform);
                    spawned.Disable();
                    container.AddProjectile(spawned);
                }
            }
        }

        public Projectile GetObject(SpellType spellType)
        {
            Projectile projectile = null;

            foreach (ProjectileContainer container in _containers)
            {
                if (container.SpellType == spellType)
                {
                    projectile = container.GetProjectile();
                    break;
                }
            }

            return projectile;
        }
    }
}
