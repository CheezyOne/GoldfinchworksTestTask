using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] private Transform _wire;
    [SerializeField] private Transform _firstBase;
    [SerializeField] private Transform _secondBase;
    [SerializeField] private float _wireColliderRadius;

    private void Update()
    {
        UpdateWireTransform();
    }

    private void UpdateWireTransform()
    {
        Vector3 direction = _firstBase.position - _secondBase.position;
        float distance = direction.magnitude;
        _wire.position = (_secondBase.position + _firstBase.position) * _wireColliderRadius;
        _wire.rotation = Quaternion.FromToRotation(Vector3.up, direction);
        _wire.localScale = new Vector3(_wire.localScale.x, distance * _wireColliderRadius, _wire.localScale.z);
    }
}