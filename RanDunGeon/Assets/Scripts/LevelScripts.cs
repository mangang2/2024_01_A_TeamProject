using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelScripts : MonoBehaviour
{
    public Text PriceText;
    public Button LevelUpButton;
    public int Level;

    public int NowChar = 1;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        Level = GM.CharLevel[0];
    }

    // Update is called once per frame
    void Update()
    {
        PriceText.text = PriceCheck().ToString() + "G";
        

        if (Level < 40 && GM.Gold >= PriceCheck())
        {
            if (Level > 0 && Level < 10)
            {
                LevelUpButton.interactable = true;
            }
            else if (Level >= 10 && Level < 20 && GM.ClearStage >= 103)
            {
                LevelUpButton.interactable = true;
            }
            else if (Level >= 20 && Level < 30 && GM.ClearStage >= 105)
            {
                LevelUpButton.interactable = true;
            }
            else if (Level >= 30 && GM.ClearStage >= 205)
            {
                LevelUpButton.interactable = true;
            }
            else
            {
                LevelUpButton.interactable = false;
            }
        }
        else
        {
            LevelUpButton.interactable = false;
        }
    }

    public void LevelUp()
    {
        if (Level < 40 && GM.Gold >= PriceCheck())
        {
            if (Level > 0 && Level < 10)
            {
                GM.Gold -= PriceCheck();
                GM.CharLevel[0] = ++Level;
                GM.LoadAllStatus();
            }
            else if (Level >= 10 && Level < 20 && GM.ClearStage >= 103)
            {
                GM.Gold -= PriceCheck();
                GM.CharLevel[0] = ++Level;
                GM.LoadAllStatus();
            }
            else if (Level >= 20 && Level < 30 && GM.ClearStage >= 105)
            {
                GM.Gold -= PriceCheck();
                GM.CharLevel[0] = ++Level;
                GM.LoadAllStatus();
            }
            else if (Level >= 30 && GM.ClearStage >= 205)
            {
                GM.Gold -= PriceCheck();
                GM.CharLevel[0] = ++Level;
                GM.LoadAllStatus();
            }
        }
    }

    private int PriceCheck()
    {
        if (Level > 0)
        {
            return Level * 250;
        }
        else if (Level > 10)
        {
            return Level * 500;
        }
        else if (Level > 20)
        {
            return Level * 750;
        }
        else
        {
            return Level * 1000;
        }
    }
}
