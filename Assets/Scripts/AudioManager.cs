using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager I { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Load()
    {
        var prefab = Resources.Load<GameObject>("AudioManager");
        var obj = Instantiate(prefab);
        DontDestroyOnLoad(obj);
    }

    [SerializeField] private AudioReferences refs;
    public AudioReferences Refs => refs;

    public void OnEnable()
    {
        I = this;
    }

    public void OnDisable()
    {
        I = null;
    }

    public void Play(AudioDetails clip) => Play(clip, Vector3.zero);


    public void Play(AudioDetails clip, Vector3 position)
    {
        var actualClip = clip.GetClip();
        var newObj = new GameObject(actualClip.name);
        newObj.transform.position = position;
        var audioSource = newObj.AddComponent<AudioSource>();
        audioSource.clip = actualClip;
        audioSource.spatialBlend = 1f;
        audioSource.volume = 1f;
        audioSource.outputAudioMixerGroup = clip.mixer;
        audioSource.Play();
        Destroy(newObj, actualClip.length);
    }

}
