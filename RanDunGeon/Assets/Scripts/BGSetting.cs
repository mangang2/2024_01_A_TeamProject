using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSetting : MonoBehaviour
{
    public Sprite[] BackGroundImage;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;

        GetComponent<SpriteRenderer>().sprite = BackGroundImage[GM.nowChapter - 1];
    }

}
