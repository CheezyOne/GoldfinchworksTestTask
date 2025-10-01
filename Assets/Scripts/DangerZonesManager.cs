using System.Collections.Generic;
using UnityEngine;

public class DangerZonesManager : Singleton<DangerZonesManager>
{
    private List<Collider> _dangerZoneColliders = new List<Collider>();

    public void RegisterDangerZone(Collider dangerZoneCollider)
    {
        if (!_dangerZoneColliders.Contains(dangerZoneCollider))
            _dangerZoneColliders.Add(dangerZoneCollider);
    }

    public void UnregisterDangerZone(Collider dangerZoneCollider)
    {
        _dangerZoneColliders.Remove(dangerZoneCollider);
    }

    public float GetDistanceToNearestDangerZone(Vector3 position)
    {
        if (_dangerZoneColliders.Count == 0)
            return Mathf.Infinity;

        float nearestDistance = Mathf.Infinity;

        foreach (Collider dangerZoneCollider in _dangerZoneColliders)
        {
            if (dangerZoneCollider == null)
                continue;

            Vector3 closestPoint = dangerZoneCollider.ClosestPoint(position);
            float distance = Vector3.Distance(position, closestPoint);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
            }
        }

        return nearestDistance;
    }
}