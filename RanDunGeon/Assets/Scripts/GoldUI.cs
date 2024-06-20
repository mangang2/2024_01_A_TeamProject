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
        string goldTemp;
        long gold = GM.Gold;

        goldTemp = gold.ToString();

        if (goldTemp.Length > 9)
        {
            returnText = goldTemp.Substring(0, goldTemp.Length - 9) + "," + goldTemp.Substring(goldTemp.Length - 9, 3) + "," + goldTemp.Substring(goldTemp.Length - 6, 3) + "," + goldTemp.Substring(goldTemp.Length - 3, 3) + "G";
        }
        else if (goldTemp.Length > 6)
        {
            returnText = goldTemp.Substring(0, goldTemp.Length - 6) + "," + goldTemp.Substring(goldTemp.Length - 6, 3) + "," + goldTemp.Substring(goldTemp.Length - 3, 3) + "G";
        }
        else if (goldTemp.Length > 3)
        {
            returnText = goldTemp.Substring(0, goldTemp.Length - 3) + "," + goldTemp.Substring(goldTemp.Length - 3, 3) + "G";
        }
        else
            returnText = goldTemp + "G";

        return returnText;
    }
}
