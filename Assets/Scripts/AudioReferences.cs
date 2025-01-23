using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioReferences", menuName = "Reef Defender/Audio References")]
public class AudioReferences : ScriptableObject
{
    public AudioDetails BubblePop;
    public AudioDetails BubblePush;
    public AudioDetails EnemyDeath;
    public AudioDetails StageRestart;
    public AudioDetails StageStart;
    public AudioDetails FishDockOn;
    public AudioDetails FishDockOff;
    public AudioDetails LevelClear;
    public AudioDetails GameOver;
    
    public AudioDetails BGMusic;
    public AudioDetails BGFXWaves;
}