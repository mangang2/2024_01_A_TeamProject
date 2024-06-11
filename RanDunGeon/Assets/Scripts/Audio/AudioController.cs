using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private string AudioNameIndex;

    [SerializeField]
    private AudioMixer audioMixer;

    private Slider volumeSlider;


    //슬라이더 Minvalue을 0.001

    private void Awake()
    {
        float nowVolume;
        volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(SetVolume);
        audioMixer.GetFloat(AudioNameIndex,out nowVolume);
        volumeSlider.value = (nowVolume +40)/5;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(AudioNameIndex, -40 + volume * 5);
    }

}
