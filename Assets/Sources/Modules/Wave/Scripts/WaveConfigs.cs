using System.Collections.Generic;
using Sources.Modules.Enemy;

namespace Sources.Modules.Wave.Scripts
{
    public class WaveConfigs
    {
        private readonly List<WaveConfig> _wavesConfigs = new List<WaveConfig>
        {
            new(
                new List<EnemyType>()
                {
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
                    EnemyType.Orc3
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Orc4,
                    EnemyType.Orc5
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Undead1,
                    EnemyType.Undead2,
                    EnemyType.Undead3,
                    EnemyType.Other3
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Undead4
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Undead5,
                    EnemyType.Undead6
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Undead7
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Other2
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon1
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon2
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon3
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Demon4,
                    EnemyType.Demon5
                }
            ),
            new(
                new List<EnemyType>()
                {
                    EnemyType.Other1
                }
            )
        };

        public WaveConfig GetWaveConfig(int index)
        {
            return _wavesConfigs[index];
        }

        public int GetWaveConfigsCount() => _wavesConfigs.Count;
        
    }
}