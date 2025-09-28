using UnityEngine;

public class RemoteController : MonoBehaviour
{
    [SerializeField] private Transform _wire;
    [SerializeField] private Transform _controllerBase;
    [SerializeField] private Transform _engineBase;
    [SerializeField] private float _wireColliderRadius;

    private void Update()
    {
        UpdateWireTransform();
    }

    private void UpdateWireTransform()
    {
        Vector3 direction = _controllerBase.position - _engineBase.position;
        float distance = direction.magnitude;
        _wire.position = (_engineBase.position + _controllerBase.position) * _wireColliderRadius;
        _wire.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        _wire.localScale = new Vector3(_wire.localScale.x, distance * _wireColliderRadius, _wire.localScale.z);
    }
}