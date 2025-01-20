using System;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    public DangerZone dangerZone;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

}
