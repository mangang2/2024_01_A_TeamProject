using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GoldUI : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public GameManager GM;

    private void Start()
    {
        GoldText = GetComponent<TextMeshProUGUI>();
        GM = GameManager.Instance;
    }

    void Update()
    {
        GoldText.text = GoldCheck();
    }

    private string GoldCheck()
    {
        string returnText;
        long gold = GM.Gold;                        //1234567
        long temp1, temp2, temp3, temp4, temp5, temp6;

        returnText = "";

        temp1 = gold % 1000;                       //567 

        temp2 = (gold - temp1)/1000;              //(1234567 - 567) / 1000 = 1234

        temp3 = temp2 % 1000;                       //234

        temp4 = (temp2 - temp3)/1000;               //(1234 - 234) / 1000 = 1


        if (temp4 > 0)
        {
            returnText += temp4 + ",";
            if (temp3 > 0)
            {
                returnText += temp3 + ",";
            }
            else
            {
                returnText += "000,";
            }

            if (temp1 > 0)
            {
                returnText += temp1 + "G";
            }
            else
            {
                returnText += "000G";
            }
        }
        else if (temp3 > 0)
        {
            returnText += temp3 + ",";
            if (temp1 > 0)
            {
                returnText += temp1 + "G";
            }
            else
            {
                returnText += "000G";
            }
        }
        else
        returnText += temp1 + "G";

        return returnText;
    }
}
