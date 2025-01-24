using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public float radius = 1f;
    [SerializeField] private Color gizmoColor;
    private HealthManager healthManager;
    [SerializeField] private Material coralMaterial;

    private void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
        healthManager.OnHealthRemainingChanged += UpdateHealth;
        coralMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    private void UpdateHealth()
    {
        var delta = healthManager.HealthRemaining / healthManager.InitialHealth;
        Debug.Log($"delta {delta}");
        coralMaterial.SetFloat("_Delta", delta);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, radius);
    }

}
