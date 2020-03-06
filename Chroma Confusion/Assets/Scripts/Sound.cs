using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;

    [HideInInspector] public AudioSource source;

    public AudioClip clip;
    
    [Range(0, 1)] public float volume;

    [Range(1, 3)] public float pitch;

    public bool playOnAwake;

    public bool loop;
}
