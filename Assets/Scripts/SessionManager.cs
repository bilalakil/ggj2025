using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SessionManager : MonoBehaviour
{
    public static SessionManager I { get; private set; }
    private bool isPlaying;
    public event Action<bool> OnPlayingStateChanged;

    public bool IsPlaying
    {
        get => isPlaying;
        set
        {
            if (value == isPlaying) return;
            isPlaying = value;
            OnPlayingStateChanged?.Invoke(value);
        }
    }
    
    public event Action OnReset;

    public void OnEnable()
    {
        I = this;
    }

    public void OnDisable()
    {
        I = null;
    }

    public void Reset() => OnReset?.Invoke();
}
