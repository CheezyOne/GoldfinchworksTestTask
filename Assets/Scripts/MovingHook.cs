using UnityEngine;

public class MovingHook : MovingPart
{
    [SerializeField] private Transform _rotatingTube;
    [SerializeField] private float _rotationSpeed = 90f;

    private bool _isRotating = false;
    private Vector3 _currentRotationAxis = Vector3.right;
    private float _currentRotationDirection;

    protected override void Update()
    {
        base.Update();

        if (_isRotating && _rotatingTube != null)
        {
            _rotatingTube.Rotate(_currentRotationAxis, _currentRotationDirection * _rotationSpeed * Time.deltaTime);
        }
    }

    private void StartPipeRotation(Direction direction)
    {
        if (_rotatingTube == null)
            return;

        _currentRotationDirection = GetRotationDirectionForDirection(direction);
        _isRotating = true;
    }

    private void StopPipeRotation()
    {
        _isRotating = false;
    }

    private float GetRotationDirectionForDirection(Direction direction)
    {
        for (int i = 0; i < _destinations.Length; i++)
        {
            if (_destinations[i].Direction == direction)
            {
                return i == 0 ? 1f : -1f;
            }
        }

        return 0f;
    }

    protected override void StartMoving(Direction direction)
    {
        base.StartMoving(direction);
        StartPipeRotation(direction);
    }

    protected override void StopMoving(Direction direction)
    {
        base.StopMoving(direction);

        if (direction == _currentDirection)
        {
            StopPipeRotation();
        }
    }

    protected override void CompleteMovement()
    {
        base.CompleteMovement();
        StopPipeRotation();
    }
}