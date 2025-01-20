using System;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public DangerZone dangerZone;
    public Color gizmoColor;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}
