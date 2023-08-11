using System;
using UnityEngine;
using Pathfinding;

namespace Sources.Modules.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private AIPath _aiPath;
        [SerializeField] private Transform _gfxTransform;
        private void Update()
        {
            TryFlip();
        }

        private void TryFlip()
        {
            if (_aiPath.desiredVelocity.x >= 0.01f)
            {
                _gfxTransform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if(_aiPath.desiredVelocity.x <= -0.01f)
            {
                _gfxTransform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }
}
