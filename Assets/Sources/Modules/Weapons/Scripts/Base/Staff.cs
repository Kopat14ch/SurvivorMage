using System.Collections.Generic;
using Sources.Modules.Finder.Scripts;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private List<SpellCaster> _spellCasterPrefabs;
        [SerializeField] private ShootPoint _shootPoint;

        private List<SpellCaster> _spellCasters;
        private FinderCloseEnemy _finder;
        private ProjectilesPool _projectilesPool;

        public void Init(FinderCloseEnemy finderCloseEnemy, ProjectilesPool projectilesPool)
        {
            _spellCasters = new List<SpellCaster>();
            _projectilesPool = projectilesPool;
            _finder = finderCloseEnemy;

            foreach (SpellCaster prefab in _spellCasterPrefabs)
            {
                SpellCaster spawned = Instantiate(prefab, transform.position, Quaternion.identity, transform);
                spawned.Init(_shootPoint, _finder, _projectilesPool);
                _spellCasters.Add(spawned);
            }
            
            StartShooting();
        }

        private void StartShooting()
        {
            foreach (SpellCaster spellCaster in _spellCasters)
                spellCaster.StartCasting();
        }
    }
}
