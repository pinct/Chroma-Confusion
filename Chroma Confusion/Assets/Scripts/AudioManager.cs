using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] soundsReference;
    public static Sound[] copiedSounds;

    // Start is called before the first frame update
    void Start()
    {
        PlaySound("BackgroundMusic", false); ;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        copiedSounds = soundsReference;

        foreach (Sound s in copiedSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public static void PlaySound(string soundToFind, bool isStandalone)
    {
        Sound s = System.Array.Find(copiedSounds, sound => sound.Name == soundToFind);

        if (s != null)
        {
            if (!isStandalone)
            {
                s.source.Play();
            }

            else
            {
                s.source.PlayOneShot(s.source.clip);
            }
        }
    }

    public static Sound GetSound(string soundToFind)
    {
        Sound s = System.Array.Find(copiedSounds, sound => sound.Name == soundToFind);
        
        if(s == null)
        {
            return null;
        }

        return s;
    }

    public static void StopSound(string soundToFind)
    {
        Sound s = System.Array.Find(copiedSounds, sound => sound.Name == soundToFind);

        if (s != null)
        {
            s.source.Stop();
        }
    }
}
