using Sources.Modules.Enemy;
using Sources.Modules.Weapons.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Base
{
    internal class Snowball : Projectile
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyUnit enemy))
            {
                enemy.TakeDamage(Damage);
            }

            gameObject.SetActive(false);
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