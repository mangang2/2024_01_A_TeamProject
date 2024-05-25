using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemStatus : MonoBehaviour
{
    public int ItemType;
    public float ItemValue;
    public bool Used;
    public float ItemAdd = 0;
    public bool EnhanceType = false;

    public bool tempObject = false;

    public ItemStatusClass Origin;

    private Button button;
    private Text text;
  

    // Start is called before the first frame update
    void Start()
    {
        if (!tempObject)
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(ClickOn);
            text = transform.parent.GetComponentInChildren<Text>();
        }

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
        if(!tempObject)
        {
            string tempT;
            if (EnhanceType)
             tempT = ItemAdd.ToString() + "%";
            else
            tempT = "+" + ItemAdd.ToString();

            if(Used)
            {
                tempT += "*";
            }
            text.text = tempT;
        }
    }

    public void SetValue(int type,float value, bool used)
    {
        ItemType = type;
        ItemValue = value;
        
        this.Used = used;
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

    public void ClickOn()
    {
        GameManager GM = GameManager.Instance;
        if (this.Used)
        {
            Debug.Log("∫Ò»∞º∫»≠ µ ");
            this.Used = false;
            Origin.Used = false;
            GM.UsingItemCheck();
        }
        else if(!this.Used && GM.NowUsingItemCount < (GM.CharLevel[GM.NowChar - 1] - 10))
        {
            Debug.Log("»∞º∫»≠ µ ");
            this.Used = true;
            Origin.Used = true;
            GM.UsingItemCheck();
        }
    }
}
