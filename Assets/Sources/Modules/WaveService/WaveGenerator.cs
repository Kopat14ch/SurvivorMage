using System;
using System.Collections.Generic;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory;
using Sources.Modules.Finder;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.WaveService
{
    public class WaveGenerator : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private FindCloseEnemy _finder;
        [SerializeField] private Weapon _weapon;
        
        [SerializeField] private EnemyType _enemyType1;
        [SerializeField] private int _enemyCount1;
        [SerializeField] private EnemyType _enemyType12;
        [SerializeField] private int _enemyCount12;
        
        [SerializeField] private EnemyType _enemyType2;
        [SerializeField] private int _enemyCount2;
        [SerializeField] private EnemyType _enemyType22;
        [SerializeField] private int _enemyCount22;
        [SerializeField] private EnemyType _enemyType23;
        [SerializeField] private int _enemyCount23;

        public event Action WaveStarted;
        public event Action WaveEnded;
        
        private List<EnemyUnit> _currentWave;
        private Dictionary<EnemyType, int> _wave1;
        private Dictionary<EnemyType, int> _wave2;

        private void OnEnable()
        {
            _wave1 = new Dictionary<EnemyType, int>()
            {
                {_enemyType1, _enemyCount1},
                {_enemyType12, _enemyCount12}
            };
            _wave2 = new Dictionary<EnemyType, int>()
            {
                {_enemyType2, _enemyCount2},
                {_enemyType22, _enemyCount22},
                {_enemyType23, _enemyCount23}
            };

            StartWave(_wave1);
        }

        private void StartWave(Dictionary<EnemyType, int> wave)
        {
            _currentWave = _spawner.SpawnEnemies(wave);

            foreach (EnemyUnit unit in _currentWave)
            {
                unit.Died += OnUnitDied;
            }
            
            _finder.SetEnemyList(_currentWave);
            _weapon.StopShooting = false;
            //WaveStarted?.Invoke();
        }
        
        private void OnUnitDied(EnemyUnit unit)
        {

            unit.Died -= OnUnitDied;
            _currentWave.Remove(unit);

            if (_currentWave.Count == 0)
            {
                EndWave();
            }
        }
        
        private void EndWave()
        {
            _weapon.StopShooting = true;
            StartWave(_wave2);
            //WaveEnded?.Invoke();
        }
    }
}
