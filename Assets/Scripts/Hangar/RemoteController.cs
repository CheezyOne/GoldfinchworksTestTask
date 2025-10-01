using UnityEngine;

public class RemoteController : MonoBehaviour
{
    private Vector3 _startLocalPosition;

    private void Awake()
    {
        _startLocalPosition = transform.localPosition;
    }

    public void OnDrop() //We're assuming that controller is attached to engine, but i'm not sure if that's how it should work. Let's pretend that rope pulls the controller back to it's state when released, or it's leash is small and the worker goes with controller in his hands while working
    {
        transform.localPosition = _startLocalPosition;
    }
}