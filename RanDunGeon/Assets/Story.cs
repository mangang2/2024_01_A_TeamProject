using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public GameObject[] SToryCut = new GameObject[4];

    private int num;
    // Start is called before the first frame update
    void Start()
    {
        num = 1;
        SToryCut[0].SetActive(true);
        AudioManager.instance.PlaySound("CardDraw");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (num < 4)
            {
                SToryCut[num++].SetActive(true);
                AudioManager.instance.PlaySound("CardDraw");
            }
            else
            {
                LoadSceneController.LoadScene("GameScene");
            }
        }
    }
}
