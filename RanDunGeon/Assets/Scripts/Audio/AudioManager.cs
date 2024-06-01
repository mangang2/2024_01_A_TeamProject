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

    public void PlaySound(string name)
    {
        Sound soundToPlay = sounds.Find(sound => sound.name == name);

        if (soundToPlay != null)
        {
            soundToPlay.audioSources.PlayOneShot(soundToPlay.audioSources.clip);
        }
        else
        {
            Debug.LogWarning("사운드 " + name + " 가 없습니다.");
        }
    }

}
