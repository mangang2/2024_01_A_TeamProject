using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    private AudioManager AD;
    // Start is called before the first frame update
    void Start()
    {
        AD = AudioManager.instance;
    }
    
    public void Click()
    {
        AD.PlaySound("Click");
    }
}
