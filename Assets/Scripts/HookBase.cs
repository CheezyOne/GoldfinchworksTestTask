using UnityEngine;

public class HookBase : MonoBehaviour
{
    [SerializeField] private Transform _hookBase;
    [SerializeField] private Transform _hookHolder;
    [SerializeField] private LineRenderer _hookWireLine;

    private void Update()
    {
        _hookWireLine.SetPosition(0, _hookBase.position);
        _hookWireLine.SetPosition(1, _hookHolder.position);
    }
}