using System.Collections;
using Sources.Modules.Common;
using Sources.Modules.Player.Animation;
using UnityEngine;

namespace Sources.Modules.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flipper))]
    [RequireComponent(typeof(Animator))]
    internal class PlayerMovement : MonoBehaviour
    {
        private const float MinMoveDirection = 0.1f;

        private float _speed;
        private Flipper _flipper;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private PlayerInput _playerInput;
        private Vector2 _moveDirection;
        private Coroutine _moveWork;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _flipper = GetComponent<Flipper>();
            _animator = GetComponent<Animator>();
            
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

        public void SetSpeed(float speed)
        {
            _speed = speed;
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
            _animator.Play(PlayerAnimator.States.Run);
            
            while (_moveDirection.magnitude > MinMoveDirection)
            {
                _rigidbody2D.velocity = _speed * _moveDirection;
                _flipper.TryFlip(_rigidbody2D.velocity.x);

                SetMoveDirection();

                yield return null;
            }

            _rigidbody2D.velocity = Vector2.zero;
            _animator.Play(PlayerAnimator.States.Idle);
        }

        private void SetMoveDirection() => _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
    }
}
