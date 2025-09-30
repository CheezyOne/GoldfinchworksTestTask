using UnityEngine;

public enum Direction
{
    up,
    down, 
    left, 
    right,
    forward,
    back
}

public class RemoteControllerButton : MonoBehaviour
{
    [SerializeField] private Direction _direction;

    public void OnPointerDown()
    {
        EventBus.OnControllerButtonPressed?.Invoke(_direction);
    }

    public void OnPointerUp()
    {
        EventBus.OnControllerButtonReleased?.Invoke(_direction);
    }
}