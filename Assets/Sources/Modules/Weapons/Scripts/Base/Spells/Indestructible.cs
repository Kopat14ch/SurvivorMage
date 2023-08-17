using Sources.Modules.Enemy;
using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Base.Spells
{
    public class Indestructible : Projectile
    {
        [SerializeField] private int _enemyCollisionsNeedToDestroy;

        private int _currentEnemyCollisionsCount = 0;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            bool enemyReceived = other.gameObject.TryGetComponent(out EnemyUnit enemy);

            if (enemyReceived)
            {
                enemy.TakeDamage(Damage);
                _currentEnemyCollisionsCount++;

                if (_currentEnemyCollisionsCount == _enemyCollisionsNeedToDestroy)
                {
                    _currentEnemyCollisionsCount = 0;
                    gameObject.SetActive(false);
                }
            }
        }
        
        public override void TryLaunch(ShootPoint shootPoint, Vector3 position)
        {
            _currentEnemyCollisionsCount = 0;
            
            if ((Vector2.Distance(position, shootPoint.transform.position) <= DistanceToLaunch ))
            {
                gameObject.SetActive(true);
                
                ShootPoint = shootPoint;
                transform.position = ShootPoint.GetPosition();

                if (DisablingWork != null)
                    StopCoroutine(DisablingWork);

                DisablingWork = StartCoroutine(Disabling());
                
                transform.up = (Vector3)(position - transform.position).normalized;;
                StartCoroutine(ChangingPosition(position));
            }
        }
    }
}
