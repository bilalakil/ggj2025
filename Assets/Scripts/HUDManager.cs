using UnityEngine;

/// <summary>
/// Contains dependencies used by children inside the HUD to help with dependency injection.
/// </summary>
public class HUDManager : MonoBehaviour
{
    public HealthManager healthManager;
}