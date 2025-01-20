using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private DangerZone dangerZone;
    private Coroutine goToTarget;
    private Vector3 target = Vector3.zero;
    private float offset = 0.1f;

    void Start()
    {
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
