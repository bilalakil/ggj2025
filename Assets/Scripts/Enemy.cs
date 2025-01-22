using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public DangerZone dangerZone;
    public HealthManager healthManager;

    private Vector3 target = Vector3.zero;
    private bool inDangerZone = false;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float speed = 1f;
    //[SerializeField] private float angleCorrection = -90f;
    [SerializeField] private float damageInterval = 2.0f;
    [SerializeField] private float damage = 2.0f;
    private float currentTimeInDangerZone;
    
    public event Action<Enemy> OnDisabled;
    
    public void OnDisable()
    {
        OnDisabled?.Invoke(this);
    }

    public void Initialise(DangerZone dangerZone, HealthManager healthManager)
    {
        this.dangerZone = dangerZone;
        this.healthManager = healthManager;
        target = CalculateNewTarget(dangerZone.transform.position, dangerZone.radius);
    }

    private Vector3 CalculateNewTarget(Vector3 origin, float radius)
    {
        var nextTarget = Random.insideUnitCircle * radius;
        return new Vector3(nextTarget.x, 0, nextTarget.y) + origin;
    }

    private float CalculateTimeInDangerZone(float delta)
    {
        if (currentTimeInDangerZone >= damageInterval)
        {
            healthManager.TakeHit(damage);
            return 0.0f;
        }

        return currentTimeInDangerZone + delta;
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

        inDangerZone = Vector3.Distance(dangerZone.transform.position, transform.position) <= dangerZone.radius;
        if (inDangerZone)
        {
            currentTimeInDangerZone = CalculateTimeInDangerZone(Time.deltaTime);
        }
    }

}
