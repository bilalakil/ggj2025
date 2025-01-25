using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSound : MonoBehaviour
{
    private Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnEnable()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void OnDisable()
    {
        button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        AudioManager.I.Play(AudioManager.I.Refs.FishDockOn);
    }
}
