using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayAndResetButton : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    
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
        if (SessionManager.I.IsPlaying)
        {
            UserCommands.ResetSession();
            AudioManager.I.Play(AudioManager.I.Refs.StageRestart, transform.position);
        }
        else
        {
            UserCommands.StartPlaying();
            AudioManager.I.Play(AudioManager.I.Refs.StageStart, transform.position);
        }
    }

    private void HandlePlayingStateChanged(bool newValue)
    {
        if (buttonText != null)
        {
            buttonText.text = newValue ? "Reset" : "Start";
        }
    }
}