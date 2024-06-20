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
            if(GM.nowChapter == 1 || GM.nowChapter == 0)
            {
                AD.PlaySound("Chapter_1_BGM", BGM);
            }
            else if(GM.nowChapter == 2)
            {
                AD.PlaySound("Chapter_2_BGM", BGM);
            }
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
