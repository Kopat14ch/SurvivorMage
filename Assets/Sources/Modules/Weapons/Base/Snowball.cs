using Sources.Modules.Common;
using Sources.Modules.Enemy;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Base
{
    internal class Snowball : Projectile
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            bool enemyReceived = other.gameObject.TryGetComponent(out EnemyUnit enemy);
            bool obstacleReceived = other.gameObject.TryGetComponent<Obstacle>(out _);
            
            if (enemyReceived)
            {
                enemy.TakeDamage(Damage);
            }

            if (obstacleReceived || enemyReceived)
            {
                gameObject.SetActive(false);
            }
        }

        public override void Launch(ShootPoint shootPoint, Vector3 position)
        {
            gameObject.SetActive(true);
            
            ShootPoint = shootPoint;
            transform.position = ShootPoint.GetPosition();

            if (DisablingWork != null)
                StopCoroutine(DisablingWork);

            DisablingWork = StartCoroutine(Disabling());
            
            StartCoroutine(ChangingPosition(position));
        }
    }
}