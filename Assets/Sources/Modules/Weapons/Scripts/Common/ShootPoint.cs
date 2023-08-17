﻿using UnityEngine;

namespace Sources.Modules.Weapons.Scripts.Common
{
    public class ShootPoint : MonoBehaviour
    {
        [SerializeField] private Transform _rotationCenter;
        public Vector3 GetPosition() => transform.position;

        public Transform GetRotationCenter() => _rotationCenter;
    }
}