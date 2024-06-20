using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardInfoUI : MonoBehaviour
{
    public Image SkillImage;
    public TextMeshProUGUI Name, Info;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void UIOn(Sprite skillImage, string cardname, string cardinfo)
    {
        SkillImage.sprite = skillImage;
        Name.text = cardname;
        Info.text = cardinfo;
    }

    public void Click()
    {
        gameObject.SetActive(false);
    }
}
