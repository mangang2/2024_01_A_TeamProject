using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemListGen : MonoBehaviour
{
    public GameObject ItemListPrefabs;

    public GameObject ItemListGroup;

    public GameObject Origin;

    public int Type;

    private GameManager GM;

    private List<ItemStatus> NowItemList = new List<ItemStatus>();

    private Vector3 FirstPos;

    private Transform LastItemPos;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        FirstPos = transform.transform.GetComponent<RectTransform>().position;
        
        GenList();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        GenList();          //나중에 삭제

        if (GetComponent<RectTransform>().position.y < FirstPos.y)
        {
            transform.GetComponent<RectTransform>().position = new Vector2(transform.GetComponent<RectTransform>().position.x, transform.GetComponent<RectTransform>().position.y + 3);
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                transform.GetComponent<RectTransform>().position = new Vector2(transform.GetComponent<RectTransform>().position.x, transform.GetComponent<RectTransform>().position.y + Input.GetAxis("Mouse ScrollWheel") * -500);
            }
            if (LastItemPos.GetComponent<RectTransform>().position.y > FirstPos.y)
            {
                transform.GetComponent<RectTransform>().position = new Vector2(transform.GetComponent<RectTransform>().position.x, transform.GetComponent<RectTransform>().position.y - 3);
            }
        }
        
    }

    public void GenList()
    {
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
                GameObject _temp = Instantiate(Origin);
                _temp.GetComponentInChildren<ItemStatus>().SetValue(GM.ItemList[i].ItemType, GM.ItemList[i].ItemValue, GM.ItemList[i].Used);
                _temp.GetComponentInChildren<ItemStatus>().Origin = GM.ItemList[i];
                NowItemList.Add(_temp.GetComponentInChildren<ItemStatus>());
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
            temp2.GetComponentInChildren<ItemStatus>().Origin = NowItemList[j].Origin;
        }
        LastItemPos = transform.GetChild(transform.childCount - 1);
    }

    private int compare(ItemStatus a, ItemStatus b)
    {
        return a.ItemValue > b.ItemValue ? -1 : 1;
    }
}
