using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellButton : MonoBehaviour
{
    public ItemStatus itemstatus;

    public int CellRank, CellNum;
    public bool Useable;

    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = CellRank.ToString() + " / " + CellNum.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
