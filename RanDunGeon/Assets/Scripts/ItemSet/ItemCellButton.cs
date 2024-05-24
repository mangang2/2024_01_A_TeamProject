using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellButton : MonoBehaviour
{
    public GameObject ItemListGroup;
    public ItemStatus itemstatus;

    public int CellNum;
    public bool Useable = false;

    private Button button;
    private GameManager GM;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        text = transform.parent.GetComponentInChildren<Text>();
        GM = GameManager.Instance;
        ItemListGroup = GameObject.Find("FindItemListGroup").GetComponent<FindGroup>().group;
    }

    // Update is called once per frame
    void Update()
    {
        if (CellNum <= (GM.CharLevel[GM.NowChar - 1] - 10))
        {
            Useable = true;
        }
        else
            Useable = false;
        if (GM.UsingItemList.Count >= CellNum)
        {

            itemstatus = GM.UsingItemList[CellNum - 1];

            if (itemstatus.EnhanceType)
                text.text = itemstatus.ItemAdd.ToString() + "%";
            else
                text.text = "+" + itemstatus.ItemAdd.ToString();
            
        }
        else if (Useable)
            text.text =  CellNum.ToString();
        else
            text.text = "";
    }

    public void OnClick()
    {
        if (Useable)
        {
            ItemListGroup.SetActive(true);
        }
        else
            Debug.Log("해금되지 않은 칸이야!");
    }
}
