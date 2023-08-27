using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    [HideInInspector] public static AudioManager instance;

    bool test;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = UnityEngine.Random.Range(s.minPitch, s.maxPitch);
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("You made a typo, you dumb shit: \"" + name + "\" doesn't exist, it's absolutely non-existant, just like the chance of you getting any bitches!");
            return;
        }
        s.source.Play();
    }

    public Sound Get(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("You made a typo, you dumb shit: \"" + name + "\" doesn't exist, it's absolutely non-existant, just like the chance of you getting any bitches!");
            return null;
        }
        return s;
    }

    public void UpdateSettings()
    {
        foreach (Sound s in sounds)
        {
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = UnityEngine.Random.Range(s.minPitch, s.maxPitch);
            s.source.loop = s.loop;
        }
    }
}
