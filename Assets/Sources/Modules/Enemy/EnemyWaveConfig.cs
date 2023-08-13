namespace Sources.Modules.Enemy
{
    public class EnemyWaveConfig
    {
        public EnemyType EnemyType { get; private set; }
        public int SpawnCount { get; private set; }

        public void Init(EnemyType enemyType, int spawnCount)
        {
            EnemyType = enemyType;
            SpawnCount = spawnCount;
        }
    }
}