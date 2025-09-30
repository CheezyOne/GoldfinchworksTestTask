using System;

public static class EventBus
{
    public static Action<Direction> OnControllerButtonPressed;
    public static Action<Direction> OnControllerButtonReleased;
}