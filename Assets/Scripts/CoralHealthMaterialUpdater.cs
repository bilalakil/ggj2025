using System.Collections.Generic;
using UnityEngine;

public class CoralHealthMaterialUpdater : MonoBehaviour
{
    private static readonly int DeltaProperty = Shader.PropertyToID("_Delta");
    
    private readonly List<Material> materials = new();

    public void Awake()
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(r.materials);
        }
    }

    public void OnEnable()
    {
        if (HealthManager.I == null)
        {
            Destroy(this);
            return;
        }
        
        HealthManager.I.OnHealthRemainingChanged += UpdateHealth;
        UpdateHealth();
    }

    public void OnDisable()
    {
        if (HealthManager.I != null) HealthManager.I.OnHealthRemainingChanged -= UpdateHealth;
    }

    private void UpdateHealth()
    {
        var delta = HealthManager.I.HealthRemaining / HealthManager.I.InitialHealth;
        foreach (var material in materials) material.SetFloat(DeltaProperty, delta);
    }
}