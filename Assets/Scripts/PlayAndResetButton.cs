using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayAndResetButton : MonoBehaviour
{
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnEnable()
    {
        button.onClick.AddListener(HandleClicked);
        
        SessionManager.I.OnPlayingStateChanged += HandlePlayingStateChanged;
        HandlePlayingStateChanged(SessionManager.I.IsPlaying);
    }

    public void OnDisable()
    {
        button.onClick.RemoveListener(HandleClicked);

        if (SessionManager.I != null)
        {
            SessionManager.I.OnPlayingStateChanged -= HandlePlayingStateChanged;
        }
    }

    private void HandleClicked()
    {
        if (SessionManager.I.IsPlaying) UserCommands.ResetSession();
        else UserCommands.StartPlaying();
    }

    private void HandlePlayingStateChanged(bool newValue)
    {
        transform.localScale = new Vector3(newValue ? -1 : 1, 1, 1);
    }
}