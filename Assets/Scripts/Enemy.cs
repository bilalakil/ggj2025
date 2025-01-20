using UnityEngine;

public class Enemy : MonoBehaviour
{
    public DangerZone dangerZone;

    private Vector3 target = Vector3.zero;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float angleCorrection = -90f;

    public void Initialise(DangerZone dangerZone)
    {
        this.dangerZone = dangerZone;
        target = CalculateNewTarget(dangerZone.transform.position, dangerZone.radius);
    }

    private Vector3 CalculateNewTarget(Vector3 origin, float radius)
    {
        var nextTarget = Random.insideUnitCircle * radius;
        return new Vector3(nextTarget.x, 0, nextTarget.y) + origin;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) > threshold)
        {
            var step = speed * Time.deltaTime; 
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        else
        {
            target = CalculateNewTarget(dangerZone.transform.position, dangerZone.radius);
        }
    }
}
