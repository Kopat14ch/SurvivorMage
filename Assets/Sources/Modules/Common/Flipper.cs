using UnityEngine;

namespace Sources.Modules.Common
{
    public class Flipper : MonoBehaviour
    {
        private const int FlipValueY = 180;
        private const float MinVelocityToFlip = 0.1f;

        private Vector3 _flipVector3 = new Vector3(0,FlipValueY,0);

        private bool IsFlipped => transform.rotation.y <= 0;
        
        public void TryFlip(float velocityX)
        {
            if (velocityX > MinVelocityToFlip && IsFlipped)
            {
                transform.Rotate(_flipVector3);
            }
            else if(velocityX < MinVelocityToFlip && IsFlipped == false)
            {
                transform.Rotate(-_flipVector3);
            }
        }
    }
}