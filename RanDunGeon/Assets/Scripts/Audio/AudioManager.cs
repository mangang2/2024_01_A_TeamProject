using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float volume = 1.0f;
    [Range (0f, 3f)]
    public float pitch = 1.0f;
    public bool loop;
    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource audioSources;
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<Sound> sounds = new List<Sound>();
    public AudioMixer audioMixer;
    private Sound beforeSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach(Sound sound in sounds)
        {
            sound.audioSources = gameObject.AddComponent<AudioSource>();
            sound.audioSources.clip = sound.Clip;
            sound.audioSources.volume = sound.volume;
            sound.audioSources.pitch = sound.pitch;
            sound.audioSources.loop = sound.loop;
            sound.audioSources.playOnAwake = false;
            sound.audioSources.outputAudioMixerGroup = sound.mixerGroup;
        }
    }

    public void PlaySound(string name, bool BGM = false)
    {
        Sound soundToPlay = sounds.Find(sound => sound.name == name);
        if(BGM)
        StopSound(soundToPlay.name);

        if (soundToPlay != null && beforeSound != soundToPlay)
        {
            soundToPlay.audioSources.Play();
            if (BGM == true)
            {
                beforeSound = soundToPlay;
            }
        }
        else
        {
            Debug.LogWarning("���� " + name + " �� �����ϴ�.");
        }
    }

    public void StopSound(string newSound)
    {
        if(beforeSound != null && beforeSound.name != newSound)
        beforeSound.audioSources.Stop();
        beforeSound = null;
    }
}
