using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStatus : MonoBehaviour
{
    public int ItemType;
    public float ItemValue;
    public bool Used;
    public float ItemAdd;


    private Text text;
    private bool EnhanceType;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        if (ItemValue > 20000)
        {
            EnhanceType = true;
            ItemAdd = ItemValue - 20000;
        }
        else if (ItemValue > 10000)
        {
            EnhanceType = false;
            ItemAdd = ItemValue - 10000;
        }
        ItemAdd = Mathf.Round(ItemAdd * 10f) / 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnhanceType)
        text.text = ItemAdd.ToString() + "%";
        else
            text.text = "+" + ItemAdd.ToString();
    }

    public void SetValue(int type,float value, bool used)
    {
        ItemType = type;
        ItemValue = value;
        
        Used = used;
        if (ItemValue > 20000)
        {
            EnhanceType = true;
            ItemAdd = ItemValue - 20000;
        }
        else if (ItemValue > 10000)
        {
            EnhanceType = false;
            ItemAdd = ItemValue - 10000;
        }
        ItemAdd = Mathf.Round(ItemAdd * 10f) / 10f;
    }
}
