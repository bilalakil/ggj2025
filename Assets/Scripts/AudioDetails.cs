    using System;
    using UnityEngine;
    using UnityEngine.Audio;

    [Serializable]
    public class AudioDetails
    {
        public AudioClip[] clips = Array.Empty<AudioClip>();
        public AudioMixerGroup mixer;
        public float volume = 0.5f;
        
        public AudioClip GetClip() => clips[UnityEngine.Random.Range(0, clips.Length)];
    }