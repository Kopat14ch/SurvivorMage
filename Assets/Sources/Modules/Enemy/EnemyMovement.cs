using System;
using UnityEngine;
using Pathfinding;
using Sources.Modules.Common;

namespace Sources.Modules.Enemy
{
    [RequireComponent(typeof(Flipper))]
    internal class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private AIPath _aiPath;

        private Flipper _flipper;

        private void Awake() => _flipper = GetComponent<Flipper>();

        private void Update()
        {
            _flipper.TryFlip(_aiPath.desiredVelocity.x);
        }
    }
}
