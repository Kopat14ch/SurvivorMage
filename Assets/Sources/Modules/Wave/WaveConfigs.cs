using System.Collections.Generic;
using Sources.Modules.Enemy;
using UnityEngine;

namespace Sources.Modules.Wave
{
    public class WaveConfigs
    {
        private readonly List<WaveConfig> _wavesConfigs = new List<WaveConfig>
        {
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon1,
                    
                    EnemyType.Demon5
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon2,
                    
                    EnemyType.Demon5
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon3,
                    EnemyType.Orc1
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Orc2
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Orc3,
                    EnemyType.Other1
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon4,
                    EnemyType.Other2
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Orc4,
                    EnemyType.Demon5,
                    EnemyType.Other3
                }
            )
        };

        public WaveConfigs()
        {
            SetWaves();
        }

        public WaveConfig GetWaveConfig(int index)
        {
            return _wavesConfigs[index];
        }

        public int GetWaveConfigsCount() => _wavesConfigs.Count;

        private void SetWaves()
        {
            int index = 1;

            for (int i = index; i < _wavesConfigs.Count; i++)
            {
                _wavesConfigs[i].AddEnemyTypes(_wavesConfigs[i - 1].GetEnemyTypes());
            }
        }
    }
}