using System.Collections;
using Sources.Modules.Common;
using Sources.Modules.Player.Scripts.Animation;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

namespace Sources.Modules.Player.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Flipper))]
    [RequireComponent(typeof(Animator))]
    internal class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;

        private const float IdleTick = 1;
        private const float MinMoveDirection = 0.1f;

        private bool _isHolding;
        private float _speed;
        private Flipper _flipper;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private PlayerInput _playerInput;
        private Vector2 _moveDirectionPc;
        private Vector2 _moveDirectionPhone;
        private Coroutine _moveWork;
        private Coroutine _idleWork;

        private bool CanMovePhone => _joystick.Direction.magnitude > MinMoveDirection && _isHolding;
        private bool CanMovePc => _moveDirectionPc.magnitude > MinMoveDirection;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _flipper = GetComponent<Flipper>();
            _animator = GetComponent<Animator>();

            _playerInput.Player.Move.performed += ctx => OnMove();
            _playerInput.Player.MoveHold.performed += ctx => OnMove();
            
            _playerInput.Player.MoveHold.started += ctx =>
            {
                if (ctx.interaction is HoldInteraction)
                {
                    _isHolding = true;
                }
            };
            _playerInput.Player.MoveHold.canceled += ctx =>
            {
                if (ctx.interaction is HoldInteraction)
                {
                    _isHolding = false;
                }
            };
            StartIdle();
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
            
            _moveWork = StartCoroutine(MobileMove());
        }

        private IEnumerator PcMove()
        {
            SetMoveDirectionPc();
            _animator.Play(PlayerAnimator.States.Run);

            while (CanMovePc)
            {
                _rigidbody2D.velocity = _speed * _moveDirectionPc;
                _flipper.TryFlip(_rigidbody2D.velocity.x);

                SetMoveDirectionPc();

                yield return null;
            }

            _rigidbody2D.velocity = Vector2.zero;
            StartIdle();
        }

        private IEnumerator MobileMove()
        {
            if (CanMovePhone == false)
                yield break;

            SetMoveDirectionPhone();
            _animator.Play(PlayerAnimator.States.Run);
            
            while (CanMovePhone)
            {
                _rigidbody2D.velocity = _speed * _moveDirectionPhone;
                _flipper.TryFlip(_rigidbody2D.velocity.x);
                
                SetMoveDirectionPhone();
                
                yield return null;
            }

            _rigidbody2D.velocity = Vector2.zero;
            StartIdle(false);
        }

        private void StartIdle(bool isPc = true)
        {
            if (_idleWork != null)
                StopCoroutine(_idleWork);
            
            _idleWork = StartCoroutine(isPc ? IdlePc() : IdlePhone());
        }
        
        private IEnumerator IdlePhone()
        {
            WaitForSeconds waitForSeconds = new(IdleTick);
            _animator.Play(PlayerAnimator.States.Idle);

            while (CanMovePhone == false)
            {
                _rigidbody2D.velocity = Vector2.zero;
                yield return waitForSeconds;
            }
        }

        private IEnumerator IdlePc()
        {
            WaitForSeconds waitForSeconds = new(IdleTick);
            _animator.Play(PlayerAnimator.States.Idle);

            while (CanMovePc == false)
            {
                _rigidbody2D.velocity = Vector2.zero;
                yield return waitForSeconds;
            }
        }

        private void SetMoveDirectionPc() => _moveDirectionPc = _playerInput.Player.Move.ReadValue<Vector2>();
        private void SetMoveDirectionPhone() => _moveDirectionPhone = _joystick.Direction.normalized;
    }
}
