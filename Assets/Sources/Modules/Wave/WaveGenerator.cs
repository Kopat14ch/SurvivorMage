using System.Collections.Generic;
using System.Linq;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory;
using Sources.Modules.Finder;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Wave
{
    public class WaveGenerator : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private FinderCloseEnemy _finder;
        [SerializeField] private List<EnemyUnit> _enemies;

        private const int StartMinEnemySpawn = 3;
        private const int StartMaxEnemySpawn = 6;
        private const int Step = 3;

        private HashSet<EnemyUnit> _enemiesHashSet;
        private List<EnemyWaveConfig> _enemyConfigs;
        private List<EnemyUnit> _currentWave;
        private Dictionary<EnemyType, int> _wave;

        private int _minEnemySpawn;
        private int _maxEnemySpawn;

        private void Awake()
        {
            _enemiesHashSet = new HashSet<EnemyUnit>();
            _wave = new Dictionary<EnemyType, int>();

            foreach (var enemy in _enemies)
                _enemiesHashSet.Add(enemy);

            _minEnemySpawn = StartMinEnemySpawn;
            _maxEnemySpawn = StartMaxEnemySpawn;

            SetNewWave();
            StartWave(_wave);
        }

        private void StartWave(Dictionary<EnemyType, int> wave)
        {
            _currentWave = _spawner.SpawnEnemies(wave);

            foreach (EnemyUnit unit in _currentWave)
            {
                unit.Died += OnUnitDied;
            }
            
            _finder.SetEnemyList(_currentWave);
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

        private void SetRandomUnits()
        {
            int numberVariationEnemies = Random.Range(1, _enemiesHashSet.Count + 1);
            _enemyConfigs = new List<EnemyWaveConfig>();

            for (int i = 0; i < numberVariationEnemies; i++)
            {
                EnemyWaveConfig tempEnemyWaveConfig = new();
                tempEnemyWaveConfig.Init(_enemiesHashSet.ElementAt(i).EnemyType,
                    Random.Range(_minEnemySpawn, _maxEnemySpawn));

                _enemyConfigs.Add(tempEnemyWaveConfig);
            }
        }

        private void SetNewWave()
        {
            _wave.Clear();

            SetRandomUnits();

            foreach (var enemyConfig in _enemyConfigs)
                _wave.Add(enemyConfig.EnemyType, enemyConfig.SpawnCount);
        }
        
        private void EndWave()
        {
            _minEnemySpawn += Step;
            _maxEnemySpawn += Step;
            
            SetNewWave();
            StartWave(_wave);
        }
    }
}
