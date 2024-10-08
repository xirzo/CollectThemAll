using Collect.Domain.Inputs;
using UnityEngine;

namespace Collect.Domain.Movement
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(PlayerInput))]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] [Min(0)] private float _moveSpeed = 5.0f;
		private Vector2 _movementInput;
		private PlayerInput _playerInput;

		private Rigidbody2D _rigidbody;
		private Vector2 _velocity;

		private void Awake()
		{
			TryGetComponent(out _rigidbody);
			TryGetComponent(out _playerInput);
		}

		private void Update()
		{
			CalculateVelocity();
			UpdateMovementInput();
		}

		private void FixedUpdate()
		{
			ApplyMovement();
		}

		private void UpdateMovementInput()
		{
			_movementInput = new Vector2(_playerInput.HorizontalMovement, 0);
		}

		private void CalculateVelocity()
		{
			_velocity = _movementInput * _moveSpeed;
		}

		private void ApplyMovement()
		{
			_rigidbody.velocity = _velocity;
		}
	}
}