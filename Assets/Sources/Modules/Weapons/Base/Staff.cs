using System.Collections.Generic;
using Sources.Modules.Finder;
using Sources.Modules.Weapons.Common;
using Sources.Modules.Weapons.Pools;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Weapons.Base
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private List<SpellCaster> _spellCasterPrefabs;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] private Button _startShooting;

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

            _startShooting.onClick.AddListener( StartShooting);
        }

        private void OnDisable()
        {
            _startShooting.onClick.RemoveListener(StartShooting);
        }

        private void StartShooting()
        {
            foreach (SpellCaster spellCaster in _spellCasters)
                spellCaster.StartCasting();
        }
    }
}
