using System.Collections;
using UnityEngine;

namespace Sources.Modules.Player
{
    internal class PlayerMovement : MonoBehaviour
    {
        private const float Speed = 15f;
        private const float MinMoveDirection = 0.1f;

        private PlayerInput _playerInput;
        private Vector2 _moveDirection;
        private Coroutine _moveWork;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            
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
            Vector3 newPosition; 
            float scaledMove;
            SetMoveDirection();
            
            while (_moveDirection.magnitude > MinMoveDirection)
            {
                scaledMove = Speed * Time.deltaTime;

                newPosition = new Vector3(_moveDirection.x, _moveDirection.y, 0);

                transform.position += newPosition * scaledMove;
                SetMoveDirection();
                yield return null;
            }
        }

        private void SetMoveDirection() => _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
    }
}
