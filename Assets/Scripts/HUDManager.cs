using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains dependencies used by children inside the HUD to help with dependency injection.
/// </summary>
public class HUDManager : MonoBehaviour
{
    public HealthManager healthManager;

    [SerializeField] private GameObject loseContainer;
    [SerializeField] private GameObject winContainer;

    public void OnEnable()
    {
        SessionManager.I.OnWinLoseStateChanged += HandleWinLoseStateChanged;
    }

    public void OnDisable()
    {
        if (SessionManager.I != null)
        {
            SessionManager.I.OnWinLoseStateChanged -= HandleWinLoseStateChanged;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void HandleWinLoseStateChanged(SessionManager.WinLoseState newValue)
    {
        loseContainer.SetActive(newValue == SessionManager.WinLoseState.Lose);
        winContainer.SetActive(newValue == SessionManager.WinLoseState.Win);
    }
}