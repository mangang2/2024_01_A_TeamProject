using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("사용할 카드")]
    public GameObject[] Card = new GameObject[4];

    [Header("실 적용 기초 스테이터스")]
    public float[] DefaultStatus = new float[5];

    [Header("캐릭터 기초 능력치")]
    public float[] BaseStatus = new float[5];

    [Header("실 적용 스테이터스 % 추가")]
    public float[] StatusPer = new float[3];

    [Header("캐릭터 기본 스테이터스 % 추가")]
    public float[] BaseStatusPer = new float[3];

    [Header("아이템 스테이터스 % 추가")]
    public float[] ItemStatusPer = new float[3];

    [Header("실 적용 스테이터스 정수값 추가")]
    public float[] StatusAdd = new float[5];

    [Header("캐릭터 기본 스테이터스 정수값 추가")]
    public float[] BaseStatusAdd = new float[5];

    [Header("아이템 스테이터스 정수값 추가")]
    public float[] ItemStatusAdd = new float[5];

    public int NowChar = 1;

    public int Char_1_Level = 1;

    public List<GameObject> CardList = new List<GameObject>();

    public List<bool> UnlockCard = new List<bool>();

    public static GameManager Instance { get; private set; }

    private GameData gameData = new GameData();

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
    }

    // Start is called before the first frame update
    void Start()
    {
        CardList = new List<GameObject>();
        UnlockCard = new List<bool>();

        var cardlist = Resources.LoadAll<GameObject>("Card");
        foreach (GameObject c in cardlist)
        {
            CardList.Add(c);
            UnlockCard.Add(c.GetComponent<CardState>().Unlock);
        }

        LoadData();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Slash))
        {
            SaveData();
        }
    }

    public void LoadAddStatus()
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
        SaveIngameData();
        LoadAddStatus();
        string jsonData = JsonUtility.ToJson(gameData,true);
        string path = Path.Combine(Application.dataPath, "TestSaveData.json");
        File.WriteAllText(path, jsonData);
        Debug.Log("저장 성공!");
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.dataPath, "TestSaveData.json");
        string jsonData = File.ReadAllText(path);
        gameData = JsonUtility.FromJson<GameData>(jsonData);
        LoadIngameData();
        LoadAddStatus();
        Debug.Log("불러오기 성공!");
    }

    private void SaveIngameData()
    {
        gameData.CharLevel = Char_1_Level;
        gameData.Char_1_Card = Card;
        gameData.Char_1_BaseStatus = BaseStatus;
        gameData.Char_1_BaseStatusPer = BaseStatusPer;
        gameData.Char_1_BaseStatusAdd = BaseStatusAdd;
    }

    private void LoadIngameData()
    {
        Char_1_Level = gameData.CharLevel;
        Card = gameData.Char_1_Card;
        BaseStatus = gameData.Char_1_BaseStatus;
        BaseStatusPer = gameData.Char_1_BaseStatusPer;
        BaseStatusAdd = gameData.Char_1_BaseStatusAdd;
    }

    private void Char_1_SetStatus()
    {
        DefaultStatus[0] = BaseStatus[0] + (Char_1_Level - 1) * 12;
        DefaultStatus[1] = BaseStatus[1] + (Char_1_Level - 1) * 3;
        DefaultStatus[2] = BaseStatus[2] + (Char_1_Level - 1) * 10;
        DefaultStatus[3] = BaseStatus[3] + Char_1_Level / 5 * 5;
        DefaultStatus[4] = BaseStatus[4] + Char_1_Level / 5 * 10;

        StatusPer[0] = Char_1_Level / 5 * 3 + ItemStatusPer[0];
        StatusPer[1] = Char_1_Level / 5 * 3 + ItemStatusPer[1];
        StatusPer[2] = Char_1_Level / 5 * 3 + ItemStatusPer[2];

        StatusAdd[0] = (Char_1_Level - 1) * 15 + ItemStatusAdd[0];
        StatusAdd[1] = (Char_1_Level - 1) * 5 + ItemStatusAdd[1];
        StatusAdd[2] = ItemStatusAdd[2];
        StatusAdd[3] = ItemStatusAdd[3];
        StatusAdd[4] = ItemStatusAdd[4];
    }



}

public class GameData
{
    public int CharLevel;

    [Header("사용할 카드")]
    public GameObject[] Char_1_Card = new GameObject[4];

    [Header("기초 스테이터스")]
    public float[] Char_1_BaseStatus = new float[5];

    [Header("스테이터스 % 추가")]
    public float[] Char_1_BaseStatusPer = new float[3];

    [Header("스테이터스 정수값 추가")]
    public float[] Char_1_BaseStatusAdd = new float[5];
}

