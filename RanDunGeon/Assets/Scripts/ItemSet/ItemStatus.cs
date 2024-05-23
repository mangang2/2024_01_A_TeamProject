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
    private string EnhanceType;

    // Start is called before the first frame update
    void Start()
    {
        text = transform.parent.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ItemValue.ToString() + EnhanceType;
    }

    public void SetValue(int type,float value, bool used)
    {
        ItemType = type;
        ItemValue = value;
        
        Used = used;

        if (value > 20000)
        {
            EnhanceType = "%";
            ItemAdd = ItemValue - 20000;
        }
        else if (value > 10000)
        {
            EnhanceType = "";
            ItemAdd = ItemValue - 10000;
        }
    }
}
