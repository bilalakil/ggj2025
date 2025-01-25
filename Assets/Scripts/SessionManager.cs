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
            if (isPlaying) AudioManager.I.Play(AudioManager.I.Refs.StageStart);
        }
    }
    
    public event Action OnReset;

    public enum WinLoseState
    {
        None,
        Win,
        Lose,
    }
    private WinLoseState winLoseStateBacking;

    public event Action<WinLoseState> OnWinLoseStateChanged;
    public WinLoseState CurrentWinLoseState
    {
        get => winLoseStateBacking;
        set
        {
            if (value == winLoseStateBacking) return;
            winLoseStateBacking = value;
            OnWinLoseStateChanged?.Invoke(value);
        }
    }

    public void OnEnable()
    {
        I = this;
        CurrentWinLoseState = WinLoseState.None;
        IsPlaying = false;
        Time.timeScale = 1;
    }

    public void OnDisable()
    {
        I = null;
    }

    public void Reset()
    {
        AudioManager.I.Play(AudioManager.I.Refs.StageRestart);
        CurrentWinLoseState = WinLoseState.None;
        OnReset?.Invoke();
        Time.timeScale = 1;
    }

    public void WinGame()
    {
        AudioManager.I.Play(AudioManager.I.Refs.LevelClear, transform.position);
        Time.timeScale = 0;
        CurrentWinLoseState = WinLoseState.Win;
    }

    public void LoseGame()
    {
        AudioManager.I.Play(AudioManager.I.Refs.GameOver, transform.position);
        Time.timeScale = 0;
        CurrentWinLoseState = WinLoseState.Lose;
    }
}
