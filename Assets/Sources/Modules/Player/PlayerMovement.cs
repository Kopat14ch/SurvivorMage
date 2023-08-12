using System.Collections;
using Sources.Modules.Common;
using UnityEngine;

namespace Sources.Modules.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flipper))]
    internal class PlayerMovement : MonoBehaviour
    {
        private const float Speed = 15f;
        private const float MinMoveDirection = 0.1f;

        private Flipper _flipper;
        private Rigidbody2D _rigidbody2D;
        private PlayerInput _playerInput;
        private Vector2 _moveDirection;
        private Coroutine _moveWork;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _flipper = GetComponent<Flipper>();
            
            _playerInput.Player.Move.performed += ctx => OnMove();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void OnMove()
        {
            if (_moveWork != null)
                StopCoroutine(_moveWork);

            _moveWork = StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            SetMoveDirection();
            
            while (_moveDirection.magnitude > MinMoveDirection)
            {
                _rigidbody2D.velocity = Speed * _moveDirection;
                _flipper.TryFlip(_rigidbody2D.velocity.x);

                SetMoveDirection();
                yield return null;
            }

            _rigidbody2D.velocity = Vector2.zero;
        }

        private void SetMoveDirection() => _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
    }
}
