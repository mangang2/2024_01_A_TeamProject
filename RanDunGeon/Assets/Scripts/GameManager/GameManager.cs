using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [Header("현재 캐릭터 번호")]
    public int NowChar = 1;

    [Header("캐릭터 레벨")]
    public int[] CharLevel = new int[1];

    [Header("사용할 카드")]
    public GameObject[] Card = new GameObject[4];

    [Header("실 적용 기초 스테이터스")]
    public float[] DefaultStatus = new float[6];

    [Header("캐릭터 기초 능력치")]
    public float[] BaseStatus = new float[6];

    [Header("실 적용 스테이터스 % 추가")]
    public float[] StatusPer = new float[3];

    [Header("아이템 스테이터스 % 추가")]
    public float[] ItemStatusPer = new float[3];

    [Header("실 적용 스테이터스 정수값 추가")]
    public float[] StatusAdd = new float[6];

    [Header("아이템 스테이터스 정수값 추가")]
    public float[] ItemStatusAdd = new float[6];

    [Header("카드 리스트")]
    public List<GameObject> CardList = new List<GameObject>();

    [Header("아이템 리스트")]
    public List<ItemStatusClass> ItemList = new List<ItemStatusClass>();

    [Header("사용중인 아이템 리스트")]
    public List<ItemStatus> UsingItemList = new List<ItemStatus>();

    public int NowUsingItemCount = 0;

    public static GameManager Instance { get; private set; }

    private GameData gameData = new GameData();

    private string path = Path.Combine(Application.dataPath, "TestSaveData.json");
    //private string monsterPath = Path.

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        DefaultStatus = new float[6];
        BaseStatus = new float[6];
        StatusAdd = new float[6];
    }

    private int compare(GameObject a, GameObject b)
    {
        return a.GetComponent<CardState>().cardType < b.GetComponent<CardState>().cardType ? -1 : 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        CardList = new List<GameObject>();
        ItemList = new List<ItemStatusClass>();

        var cardlist = Resources.LoadAll<GameObject>("Card");
        foreach (GameObject c in cardlist)
        {
            CardList.Add(c);
        }

        CardList.Sort(compare);

        LoadData();    

        if (File.Exists(path))
        {
            Invoke("LoadData",0.05f);
        }
        else
        {
            Debug.Log("new data");
        }

        UsingItemCheck();
    }

    

    public void UsingItemCheck()
    {


        List<ItemStatus> NowItemList = new List<ItemStatus>();
        UsingItemList.Clear();
        for (int i = 0; i < 6; i++)
        {
            int numTemp = 0;

            NowItemList.Clear();
            for (int m = 0; m < ItemList.Count; m++)
            {
                
                if (ItemList[m].ItemType == i && ItemList[m].Used == true)
                {
                    GameObject _temp = new GameObject("EX");
                    _temp.AddComponent<ItemStatus>();
                    _temp.GetComponent<ItemStatus>().tempObject = true;
                    _temp.GetComponent<ItemStatus>().SetValue(ItemList[m].ItemType, ItemList[m].ItemValue, ItemList[m].Used);

                    if (ItemList[m].ItemValue > 20000)
                    {
                        if(i <3)
                        ItemStatusPer[i] += (ItemList[m].ItemValue - 20000);
                        else
                        ItemStatusAdd[i] += (ItemList[m].ItemValue - 20000);
                    }
                    else
                    {
                        ItemStatusAdd[i] += (ItemList[m].ItemValue - 10000);
                    }
                    NowItemList.Add(_temp.GetComponent<ItemStatus>());
                    Debug.Log("새로 추가되는 아이템 - " + _temp.GetComponent<ItemStatus>().ItemType.ToString() + " / " + _temp.GetComponent<ItemStatus>().ItemValue.ToString());

                    numTemp++;
                    Destroy( _temp );
                }
                
            }




            NowItemList.Sort(compare);
            foreach (ItemStatus itemtemp in NowItemList)
            {
                UsingItemList.Add(itemtemp);
                for (int a = 0; a < UsingItemList.Count; a ++)
                {
                    Debug.Log("사용중 아이템 리스트 / " + UsingItemList[a].ItemValue.ToString() + " / 총 갯수 - " + UsingItemList.Count.ToString());
                }
            }
            
        }
        string texttemp;
        texttemp = "";

        for (int h = 0; h < ItemList.Count; h++)
        {
            if (ItemList[h].ItemType == 0)
                texttemp += ItemList[h].ItemValue.ToString() + "(" + ItemList[h].Used + ") / ";
        }

        Debug.Log("HP 전체 리스트 - " + texttemp );
    }

    private int compare(ItemStatus a, ItemStatus b)
    {
        return a.ItemValue > b.ItemValue ? -1 : 1;
    }

    private void Update()
    {
        NowUsingItemCount = UsingItemList.Count;


        if (Input.GetKeyDown(KeyCode.Slash))
        {
            SaveData();
        }

        switch (NowChar)
        {
            case 1:
                Char_1_SetStatus();
                break;

            default:
                break;
        }
    }

    public void LoadAllStatus()
    {
        switch (NowChar)
        {
            case 1:
                Char_1_SetStatus();
                break;

            default:
                break;
        }
    }


    public void SaveData()
    {
        SaveGameData();
        LoadAllStatus();
        string jsonData = JsonUtility.ToJson(gameData,true);
        File.WriteAllText(path, jsonData);
        Debug.Log("저장 성공!");
    }

    public void LoadData()
    {
        string jsonData = File.ReadAllText(path);
        gameData = JsonUtility.FromJson<GameData>(jsonData);
        Invoke("LoadGameData",0.1f);
        Char_1_SetStatus();
        Debug.Log("불러오기 성공!");
    }

    private void SaveGameData()
    {
        gameData.CharLevel[0] = CharLevel[0];
        for (int i = 0; i < 4; i++)
        {
            gameData.Char_1_Card[i] = Card[i].GetComponent<CardState>().cardType;
        }
        gameData.Char_1_BaseStatus = BaseStatus;
        gameData.Char_1_ItemType.Clear();
        gameData.Char_1_ItemValue.Clear();
        gameData.Char_1_ItemUsed.Clear();
        for (int L = 0; L < ItemList.Count; L++)
        {
            gameData.Char_1_ItemType.Add(ItemList[L].ItemType);
            gameData.Char_1_ItemValue.Add(ItemList[L].ItemValue);
            gameData.Char_1_ItemUsed.Add(ItemList[L].Used);
        }

        gameData.CardUnlock.Clear();
        for (int c = 0; c < ItemList.Count; c++)
        {
            gameData.CardUnlock.Add(CardList[c].GetComponent<CardState>().Unlock);
        }
    }

    private void LoadGameData()
    {
        CharLevel[0] = gameData.CharLevel[0];
        for (int i = 0; i < 4; i++)
        {
            Card[i] = CardList[gameData.Char_1_Card[i] - 1];
        }
        BaseStatus = gameData.Char_1_BaseStatus;
        ItemList.Clear();
        for(int L = 0; L < gameData.Char_1_ItemType.Count; L++)
        {
            ItemList.Add(new ItemStatusClass()); 
            ItemList[L].ItemType = gameData.Char_1_ItemType[L];
            ItemList[L].ItemValue = gameData.Char_1_ItemValue[L];
            ItemList[L].Used = gameData.Char_1_ItemUsed[L];
        }

        for (int c = 0; c < ItemList.Count; c++)
        {
            CardList[c].GetComponent<CardState>().Unlock = gameData.CardUnlock[c]; 
        }
    }

    private void Char_1_SetStatus()
    {
        int LevelDiveide1 = Mathf.FloorToInt(CharLevel[0] / 10);
        DefaultStatus[0] = BaseStatus[0] + (CharLevel[0] - 1) * 10;
        DefaultStatus[1] = BaseStatus[1] + (CharLevel[0] - 1) * 10;
        DefaultStatus[2] = BaseStatus[2] + (CharLevel[0] - 1) * 10;
        DefaultStatus[3] = BaseStatus[3] + LevelDiveide1 * 5;
        DefaultStatus[4] = BaseStatus[4] + LevelDiveide1 * 10;
        DefaultStatus[5] = BaseStatus[5];


        int LevelDiveide2 = Mathf.FloorToInt(CharLevel[0] / 5);
        StatusPer[0] = (LevelDiveide2 * 3 + ItemStatusPer[0]) * 0.01f;
        StatusPer[1] = (LevelDiveide2 * 3 + ItemStatusPer[1]) * 0.01f;
        StatusPer[2] = (LevelDiveide2 * 3 + ItemStatusPer[2]) * 0.01f;


        StatusAdd[0] = (CharLevel[0] - 1) * 10 + ItemStatusAdd[0];
        StatusAdd[1] = (CharLevel[0] - 1) * 5 + ItemStatusAdd[1];
        StatusAdd[2] = ItemStatusAdd[2];
        StatusAdd[3] = ItemStatusAdd[3];
        StatusAdd[4] = ItemStatusAdd[4];
        StatusAdd[5] = ItemStatusAdd[5];

    }



}

[System.Serializable]
public class GameData
{
    public int[] CharLevel = new int[1];

    public List<bool> CardUnlock = new List<bool>();

    public int[] Char_1_Card = new int[4];

    public float[] Char_1_BaseStatus = new float[6];

    public List<int> Char_1_ItemType = new List<int>();

    public List<float> Char_1_ItemValue = new List<float>();

    public List<bool> Char_1_ItemUsed = new List<bool>();

}

public class MonsterData
{

}

public class ItemStatusClass
{
    public int ItemType;

    public float ItemValue;

    public bool Used;
}

