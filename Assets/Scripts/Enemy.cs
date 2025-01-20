using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public DangerZone dangerZone;

    private Coroutine goToTarget;
    private Vector3 target = Vector3.zero;
    private float offset = 0.1f;

    public void Initialise(DangerZone dangerZone)
    {
        this.dangerZone = dangerZone;
        if (goToTarget != null)
        {
            StopCoroutine(goToTarget);
        }
        goToTarget = StartCoroutine(GoToTarget());
    }

    private void CalculateTarget()
    {

    }

    private IEnumerator GoToTarget()
    {
        while(Vector3.Distance(transform.position, target) > 0.1f)
        {
            
            yield return null;
        }
    }
}
