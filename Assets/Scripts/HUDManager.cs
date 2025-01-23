using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains dependencies used by children inside the HUD to help with dependency injection.
/// </summary>
public class HUDManager : MonoBehaviour
{
    public HealthManager healthManager;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}