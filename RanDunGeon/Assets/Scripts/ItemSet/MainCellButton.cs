using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCellButton : MonoBehaviour
{
    public int CellNum;

    public GameObject ItemListGroup;

    private GameManager GM;

    private Button button;

    private string cellName = "";

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickOn);
        text = transform.parent.GetComponentInChildren<Text>();
        switch(CellNum)
        {
            case 0:
                cellName = "Back";
                break;
            case 1:
                cellName = "HP";
                break;
            case 2:
                cellName = "AD";
                break;
            case 3:
                cellName = "DF";
                break;
            case 4:
                cellName = "CriP";
                break;
            case 5:
                cellName = "CriD";
                break;
            case 6:
                cellName = "ED";
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CellNum > 3)
        {
            text.text = $"{cellName}\n{GM.DefaultStatus[CellNum -1]} ( + {GM.StatusAdd[CellNum - 1]})";
        }
        else if (CellNum > 0)
        {
            text.text = $"{cellName}\n{GM.DefaultStatus[CellNum -1]} ( + {(GM.DefaultStatus[CellNum - 1] * (GM.StatusPer[CellNum - 1]-1) + GM.StatusAdd[CellNum - 1]).ToString("F0")})";
        }
        else if(CellNum == 0)
                text.text = cellName;
    }

    public void ClickOn()
    {
        if (CellNum != 0)
            ItemListGroup.SetActive(true);
        else
            GM.UsingItemCheck();
        
    }
}
