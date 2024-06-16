using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    public string MusicName;
    public bool BGM;

    private AudioManager AD;
    private GameManager GM;

    void Start()
    {
        AD = AudioManager.instance;
        GM = GameManager.Instance;

        if(MusicName == "Game")
        {

        }
        else if(MusicName == "None")
        {
            AD.StopSound("None");
        }
        else
        {
            AD.PlaySound(MusicName,BGM);
        }
    }


}
