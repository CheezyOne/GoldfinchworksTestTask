using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private void OnEnable()
    {
        DangerZonesManager.Instance?.RegisterDangerZone(_collider);
    }

    private void OnDisable()
    {
        DangerZonesManager.Instance?.UnregisterDangerZone(_collider);
    }
}