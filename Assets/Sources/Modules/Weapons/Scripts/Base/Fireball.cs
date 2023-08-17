using Sources.Modules.Weapons.Scripts.Common;
using UnityEngine;

namespace Sources.Modules.Weapons.Base
{
    internal class Fireball : Projectile
    {
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