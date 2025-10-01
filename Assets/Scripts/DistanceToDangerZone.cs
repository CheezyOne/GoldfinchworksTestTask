using UnityEngine;

public class DistanceToDangerZone : MonoBehaviour
{
    [SerializeField] private float _updateInterval = 0.1f;

    private float _distanceToNearestDangerZone = Mathf.Infinity;
    private float _timer;

    public string DistanceToNearestDangerZone => Mathf.Round(_distanceToNearestDangerZone) + " M.";

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _updateInterval && DangerZonesManager.Instance != null)
        {
            UpdateDangerZoneInfo();
            _timer = 0f;
        }
    }

    private void UpdateDangerZoneInfo()
    {
        _distanceToNearestDangerZone = DangerZonesManager.Instance.GetDistanceToNearestDangerZone(transform.position);
    }
}