using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSound : MonoBehaviour
{
    private AudioManager AD;

    void Start()
    {
        AD = AudioManager.instance;
    }

    public void Gold()
    {
        AD.PlaySound("Gold");
    }
}
