using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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
        if(Input.GetKeyDown(KeyCode.L))
        GenList();          //���߿� ����
    }

    public void GenList()
    {
        ItemStatusClass itemClass = new ItemStatusClass();
        Transform[] child = GetComponentsInChildren<Transform>();
        if(child != null)
        {
            for(int i = 1; i < child.Length; i++)
            {
                 Destroy(child[i].gameObject);
            }
        }
        NowItemList = new List<ItemStatus>();
        for (int i = 0; i < GM.ItemList.Count; i++ )
        {
            if (GM.ItemList[i].ItemType == Type)
            {
                GameObject _temp = new GameObject();
                ItemStatus temp = _temp.AddComponent<ItemStatus>();
                _temp.GetComponentInChildren<ItemStatus>().SetValue(GM.ItemList[i].ItemType, GM.ItemList[i].ItemValue, GM.ItemList[i].Used);
                itemClass = GM.ItemList[i];
                NowItemList.Add(temp);
                Destroy(_temp.gameObject);
            }
        }
        NowItemList.Sort(compare);

        for(int j = 0; j < NowItemList.Count; j++)
        {
            GameObject temp2 =Instantiate(ItemListPrefabs);
            temp2.transform.SetParent(ItemListGroup.transform);
            temp2.transform.localScale = new Vector3(0.6666667f, 0.6666667f, 1);
            temp2.GetComponentInChildren<ItemStatus>().SetValue(NowItemList[j].ItemType, NowItemList[j].ItemValue, NowItemList[j].Used);
            temp2.GetComponentInChildren<ItemStatus>().Origin = itemClass;
        }
    }

    private int compare(ItemStatus a, ItemStatus b)
    {
        return a.ItemValue > b.ItemValue ? -1 : 1;
    }
}
