using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnLevelWin()
    {
        AudioManager.I.Play(AudioManager.I.Refs.LevelClear, transform.position);
    }
    
    public void OnGameOver()
    {
        AudioManager.I.Play(AudioManager.I.Refs.GameOver, transform.position);
    }
    
}
