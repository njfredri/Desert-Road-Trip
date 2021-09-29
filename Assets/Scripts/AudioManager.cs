using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    private Dictionary<string, AudioSource> soundDict = new Dictionary<string, AudioSource>();
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            Destroy(this);
        }

        foreach (Sound s in sounds)
        {
            AudioSource current = gameObject.AddComponent<AudioSource>();
            current.name = s.Name;
            current.clip = s.source;
            current.loop = s.loop;
            current.volume = s.volume;
            soundDict.Add(s.Name, current);
        }
    }

    public void PlaySound(string name)
    {
        if (!CheckForSound(name))
        {
            Debug.Log(name + " Not found!");
            return;
        }
        if (!soundDict[name].isPlaying) soundDict[name].Play();
    }

    public void ForcePlay(string name)
    {
        if (!CheckForSound(name))
        {
            Debug.Log(name + " Not found!");
            return;
        }
        soundDict[name].PlayOneShot(soundDict[name].clip);
    }

    public void StopSound(string name)
    {
        if (!CheckForSound(name))
        {
            Debug.Log(name + " Not found!");
            return;
        }
        soundDict[name].Stop();
    }

    public void ResumeSound(string name)
    {
        if (!CheckForSound(name))
        {
            Debug.Log(name + " Not found!");
            return;
        }
        soundDict[name].UnPause();
    }

    public void PauseSound(string name)
    {
        if (!CheckForSound(name))
        {
            Debug.Log(name + " Not found!");
            return;
        }
        soundDict[name].Pause();
    }

    private bool CheckForSound(string name)
    {
        return soundDict.ContainsKey(name);
    }
}

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip source;
    public float volume;
    public bool loop;
}
