using System.Collections.Generic;
using Sources.Modules.Finder.Scripts;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Weapons.Scripts.Base
{
    public class Staff : MonoBehaviour
    {
        [SerializeField] private List<SpellCaster> _spellCasterPrefabs;
        [SerializeField] private ShootPoint _shootPoint;
        [SerializeField] private Button _startShootingButton;
        [SerializeField] private Button _stopShootingButton;

        public int ActiveSpells => _activeSpellCasters.Count;
        
        private List<SpellCaster> _spellCasters;
        private List<SpellCaster> _activeSpellCasters;
        private FinderCloseEnemy _finder;
        private ProjectilesPool _projectilesPool;

        public void Init(FinderCloseEnemy finderCloseEnemy, ProjectilesPool projectilesPool)
        {
            _spellCasters = new List<SpellCaster>();
            _activeSpellCasters = new List<SpellCaster>();
            _projectilesPool = projectilesPool;
            _finder = finderCloseEnemy;

            foreach (SpellCaster prefab in _spellCasterPrefabs)
            {
                SpellCaster spawned = Instantiate(prefab, transform.position, Quaternion.identity, transform);
                spawned.Init(_shootPoint, _finder, _projectilesPool);
                _spellCasters.Add(spawned);
            }
            
            _startShootingButton.onClick.AddListener(StartShooting);
            _stopShootingButton.onClick.AddListener(StopShooting);
        }

        public void AddSpellCaster(SpellType spellType)
        {
            foreach (SpellCaster caster in _spellCasters)
            {
                if (caster.SpellType == spellType)
                {
                    _activeSpellCasters.Add(caster);
                    break;
                }
            }
        }

        public void RemoveSpellCaster(SpellType spellType)
        {
            foreach (SpellCaster caster in _activeSpellCasters)
            {
                if (caster.SpellType == spellType)
                {
                    _activeSpellCasters.Remove(caster);
                    break;
                }
            }
        }
        
        private void OnDisable()
        {
            _startShootingButton.onClick.RemoveListener(StartShooting);
            _stopShootingButton.onClick.RemoveListener(StopShooting);
        }

        private void StartShooting()
        {
            foreach (SpellCaster spellCaster in _activeSpellCasters)
                spellCaster.StartCasting();
        }

        private void StopShooting()
        {
            foreach (SpellCaster spellCaster in _activeSpellCasters)
                spellCaster.StopCasting();
        }
    }
}
