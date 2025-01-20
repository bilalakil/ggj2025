using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float radius = 1f;
    [SerializeField] private Color gizmoColor;

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
