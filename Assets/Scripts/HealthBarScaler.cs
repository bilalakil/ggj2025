using System;
using UnityEngine;

public class HealthBarScaler : MonoBehaviour
{
    private HUDManager hudManager;
    private RectTransform rectTransform;
    private bool started;

    public void Awake()
    {
        hudManager = GetComponentInParent<HUDManager>();
        rectTransform = (RectTransform)transform;
    }

    public void OnEnable()
    {
        hudManager.healthManager.OnHealthRemainingChanged += HandleHealthRemainingChanged;
        if (started) HandleHealthRemainingChanged();
    }

    public void OnDisable()
    {
        if (hudManager != null && hudManager.healthManager != null)
        {
            hudManager.healthManager.OnHealthRemainingChanged -= HandleHealthRemainingChanged;
        }
    }

    public void Start()
    {
        started = true;
        HandleHealthRemainingChanged();
    }

    private void HandleHealthRemainingChanged()
    {
        var pctHealthRemaining = hudManager.healthManager.HealthRemaining / hudManager.healthManager.InitialHealth;
        var existingAnchorMax = rectTransform.anchorMax;
        rectTransform.anchorMax = new Vector2(
            existingAnchorMax.x, 
            Mathf.Clamp(pctHealthRemaining, 0, 1)
        );
    }
}