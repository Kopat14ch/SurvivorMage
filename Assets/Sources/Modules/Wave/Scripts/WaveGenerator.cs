using System;
using System.Collections.Generic;
using Sources.Modules.CoinFactory.Scripts;
using Sources.Modules.Enemy;
using Sources.Modules.EnemyFactory.Scripts;
using Sources.Modules.Finder.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.Wave.Scripts
{
    public class WaveGenerator : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private FinderCloseEnemy _finder;
        [SerializeField] private List<EnemyUnit> _enemiesToSpawn;
        [SerializeField] private CoinSpawner _coinSpawner;

        public event Action UnitDied;
        public event Action<int> WaveStarted; 

        private const int StartMinEnemySpawn = 3;
        private const int StartMaxEnemySpawn = 6;
        private const int Step = 3;
        
        private List<EnemyWaveConfig> _enemyConfigs;
        private List<EnemyUnit> _spawnedEnemies;
        private Dictionary<List<EnemyType>, int> _wave;

        private WaveConfigs _waveConfigs;

        private int _waveCount;
        private int _minEnemySpawn;
        private int _maxEnemySpawn;

        private void Awake()
        {
            _wave = new Dictionary<List<EnemyType>, int>();
            _waveConfigs = new WaveConfigs();
            _waveCount = 1;

            _minEnemySpawn = StartMinEnemySpawn;
            _maxEnemySpawn = StartMaxEnemySpawn;

            SetNewWave();
            StartWave(_wave);
        }

        private void StartWave(Dictionary<List<EnemyType>, int> wave)
        { 
            _spawner.SpawnEnemies(wave, _waveCount);

            _spawnedEnemies = _spawner.GetEnemies();

            foreach (EnemyUnit unit in _spawnedEnemies)
            {
                unit.Died += OnUnitDied;
            }
            
            WaveStarted?.Invoke(_spawnedEnemies.Count);
            _finder.SetEnemyList(_spawnedEnemies);
            _coinSpawner.SetEnemies(_spawnedEnemies);
        }
        
        private void OnUnitDied(EnemyUnit unit)
        {
            unit.Died -= OnUnitDied;
            _spawnedEnemies.Remove(unit);

            UnitDied?.Invoke();

            if (_spawnedEnemies.Count <= 0)
                NextWave();
        }

        private void SetRandomUnits()
        {
            int numberVariationEnemies = Random.Range(1, _enemiesToSpawn.Count + 1);
            _enemyConfigs = new List<EnemyWaveConfig>();

            for (int i = 0; i < numberVariationEnemies; i++)
            {
                _waveCount %= _waveConfigs.GetWaveConfigsCount();

                EnemyWaveConfig tempEnemyWaveConfig = new (_waveConfigs.GetWaveConfig(_waveCount -1).GetEnemyTypes());
                tempEnemyWaveConfig.Init(Random.Range(_minEnemySpawn, _maxEnemySpawn));

                _enemyConfigs.Add(tempEnemyWaveConfig);
            }
        }

        private void SetNewWave()
        {
            _wave.Clear();

            SetRandomUnits();

            foreach (var enemyWaveConfig in _enemyConfigs)
                _wave.Add(enemyWaveConfig.GetEnemyTypes(), enemyWaveConfig.SpawnCount);
        }
        
        private void NextWave()
        {
            _minEnemySpawn += Step;
            _maxEnemySpawn += Step;
            _waveCount++;
            
            SetNewWave();
            StartWave(_wave);
        }
    }
}
