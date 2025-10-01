using System;
using UnityEngine;

public class MovingPart : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _acceleration = 4f;
    [SerializeField] private float _deceleration = 8f;
    [SerializeField] protected DestinationInDirection[] _destinations;
    [SerializeField] protected AudioSource _movementAudio;

    private Vector3 _currentVelocity;
    private bool _isInertiaMoving = false;
    private Vector3 _movementDirection;
    private float _currentSpeed;
    private Transform _boundaryTransform;
    private Vector3 _boundaryWorldPosition;

    protected Direction _currentDirection;
    protected bool _isMoving = false;

    private const float MINIMUM_VELOCITY_THRESHOLD = 0.01f;
    private const float BOUNDARY_TRIGGER_DISTANCE = 0.1f;

    protected virtual void Update()
    {
        if (_isMoving && !_isInertiaMoving)
        {
            UpdateActiveMovement();
        }
        else if (_isInertiaMoving)
        {
            ApplyInertiaMovement();
        }
    }

    private void UpdateActiveMovement()
    {
        if (IsNearBoundary())
        {
            _currentVelocity = _movementDirection * _currentSpeed;
            _isInertiaMoving = true;
            _isMoving = false;
            return;
        }

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _moveSpeed, _acceleration * Time.deltaTime);
        transform.position += _movementDirection * (_currentSpeed * Time.deltaTime);
    }

    private void ApplyInertiaMovement()
    {
        if (_currentVelocity.magnitude > MINIMUM_VELOCITY_THRESHOLD)
        {
            _currentVelocity = Vector3.MoveTowards(_currentVelocity, Vector3.zero, _deceleration * Time.deltaTime);
            transform.position += _currentVelocity * Time.deltaTime;

            if (_currentVelocity.magnitude <= MINIMUM_VELOCITY_THRESHOLD)
            {
                CompleteMovement();
            }
        }
        else
        {
            CompleteMovement();
        }
    }

    private bool IsNearBoundary()
    {
        if (_boundaryTransform == null)
            return false;

        _boundaryWorldPosition = _boundaryTransform.position;
        Vector3 toBoundary = _boundaryWorldPosition - transform.position;
        float distanceToBoundary = Vector3.Dot(toBoundary, _movementDirection);
        return distanceToBoundary > 0 && distanceToBoundary <= BOUNDARY_TRIGGER_DISTANCE;
    }

    protected virtual void StartMoving(Direction direction)
    {
        if (_isMoving || _isInertiaMoving)
            return;

        if (!CanMoveInDirection(direction))
            return;

        _movementDirection = GetDirectionVector(direction);

        if (_movementDirection == Vector3.zero)
            return;

        _isMoving = true;
        _isInertiaMoving = false;
        _currentDirection = direction;
        _currentSpeed = 0f;
        StartMovementAudio();
    }

    protected virtual void StopMoving(Direction direction)
    {
        if (_isMoving && direction == _currentDirection && !_isInertiaMoving)
        {
            _currentVelocity = _movementDirection * _currentSpeed;
            _isInertiaMoving = true;
            _isMoving = false;
        }
    }

    private Vector3 GetDirectionVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.up: return Vector3.up;
            case Direction.down: return Vector3.down;
            case Direction.left: return Vector3.left;
            case Direction.right: return Vector3.right;
            case Direction.forward: return Vector3.forward;
            case Direction.back: return Vector3.back;
            default: return Vector3.zero;
        }
    }

    private bool CanMoveInDirection(Direction direction)
    {
        foreach (var destination in _destinations)
        {
            if (destination.Direction == direction)
            {
                _boundaryTransform = destination.Destination;
                _boundaryWorldPosition = _boundaryTransform.position;
                Vector3 toBoundary = _boundaryWorldPosition - transform.position;
                Vector3 directionVector = GetDirectionVector(direction);
                float distanceToBoundary = Vector3.Dot(toBoundary, directionVector);
                bool isAtOrBeyondBoundary = distanceToBoundary <= 0;
                return !isAtOrBeyondBoundary;
            }
        }

        return false;
    }

    protected void StopAllMovement()
    {
        _isMoving = false;
        _isInertiaMoving = false;
        _currentVelocity = Vector3.zero;
        _currentSpeed = 0f;
        StopMovementAudio();
    }

    protected virtual void CompleteMovement()
    {
        _isMoving = false;
        _isInertiaMoving = false;
        _currentVelocity = Vector3.zero;
        _currentSpeed = 0f;
        StopMovementAudio();
    }

    private void StartMovementAudio()
    {
        if (_movementAudio != null && !_movementAudio.isPlaying)
        {
            _movementAudio.Play();
        }
    }

    private void StopMovementAudio()
    {
        if (_movementAudio != null && _movementAudio.isPlaying)
        {
            _movementAudio.Stop();
        }
    }

    protected virtual void OnEnable()
    {
        EventBus.OnControllerButtonPressed += StartMoving;
        EventBus.OnControllerButtonReleased += StopMoving;
    }

    protected virtual void OnDisable()
    {
        EventBus.OnControllerButtonPressed -= StartMoving;
        EventBus.OnControllerButtonReleased -= StopMoving;
        StopAllMovement();
    }
}

[Serializable]
public struct DestinationInDirection
{
    public Direction Direction;
    public Transform Destination;
}