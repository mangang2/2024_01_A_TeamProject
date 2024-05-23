using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListGen : MonoBehaviour
{
    public GameObject ItemListPrefabs;

    public GameObject ItemListGroup;

    public int Type;

    private GameManager GM;

    private List<ItemStatus> NowItemList = new List<ItemStatus>();

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        GenList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenList()
    {
        for(int i = 0; i < GM.ItemList.Count; i++ )
        {
            if (GM.ItemList[i].ItemType == Type)
            {
                ItemStatus temp = new ItemStatus();
                temp.SetValue(GM.ItemList[i].ItemType, GM.ItemList[i].ItemValue, GM.ItemList[i].Used);
                NowItemList.Add(temp);
            }
        }
        NowItemList.Sort(compare);

        for(int j = 0; j < NowItemList.Count; j++)
        {
            GameObject temp2 =Instantiate(ItemListPrefabs);
            temp2.GetComponent<ItemStatus>().SetValue(NowItemList[j].ItemType, NowItemList[j].ItemValue, NowItemList[j].Used);
        }
    }

    private int compare(ItemStatus a, ItemStatus b)
    {
        return a.ItemValue < b.ItemValue ? -1 : 1;
    }
}
