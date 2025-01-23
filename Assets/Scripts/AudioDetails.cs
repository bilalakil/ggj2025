    using System;
    using UnityEngine;
    using UnityEngine.Audio;

    [Serializable]
    public class AudioDetails
    {
        public AudioClip[] clips;
        public AudioMixerGroup mixer;
        
        public AudioClip GetClip() => clips[UnityEngine.Random.Range(0, clips.Length)];
    }