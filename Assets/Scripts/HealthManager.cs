using System;
using UnityEngine;

[DefaultExecutionOrder(-99)]
public class HealthManager : MonoBehaviour
{
    public static HealthManager I { get; private set; }
    
    public float InitialHealth { get; private set; }

    private float healthRemainingBacking;
    public event Action OnHealthRemainingChanged;
    public float HealthRemaining
    {
        get => healthRemainingBacking;
        private set
        {
            if (Mathf.Approximately(value, healthRemainingBacking)) return;
            healthRemainingBacking = value;
            OnHealthRemainingChanged?.Invoke();
        }
    }

    [SerializeField] private float initialDangerZoneHealth = 5.0f;
    
    public void Awake()
    {
        InitialHealth = healthRemainingBacking = initialDangerZoneHealth;
    }

    public void OnEnable()
    {
        I = this;
        SessionManager.I.OnReset += HandleReset;
    }

    public void OnDisable()
    {
        I = null;
        if (SessionManager.I != null) SessionManager.I.OnReset -= HandleReset;
    }
    
    public void TakeHit(float damage)
    {
        HealthRemaining -= damage;
        if (HealthRemaining <= 0)
        {
            SessionManager.I.LoseGame();
        }
    }

    private void HandleReset()
    {
        HealthRemaining = InitialHealth;
    }
}
