using UnityEngine;

public class DangerZoneVisualsScaler : MonoBehaviour
{
    public void Awake()
    {
        var dangerZone = GetComponentInParent<DangerZone>();
        
        // Assuming that the radius doesn't change over time...
        var worldScale = transform.lossyScale;
        transform.localScale = new Vector3(
            dangerZone.radius / worldScale.x,
            worldScale.y,
            dangerZone.radius / worldScale.z
        );
    }
}
