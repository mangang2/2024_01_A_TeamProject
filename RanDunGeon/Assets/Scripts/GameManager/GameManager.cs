using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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

    [Header("캐릭터 기본 스테이터스 % 추가")]
    public float[] BaseStatusPer = new float[3];

    [Header("아이템 스테이터스 % 추가")]
    public float[] ItemStatusPer = new float[3];

    [Header("실 적용 스테이터스 정수값 추가")]
    public float[] StatusAdd = new float[6];

    [Header("캐릭터 기본 스테이터스 정수값 추가")]
    public float[] BaseStatusAdd = new float[6];

    [Header("아이템 스테이터스 정수값 추가")]
    public float[] ItemStatusAdd = new float[6];

    public List<GameObject> CardList = new List<GameObject>();

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

    private int compare(GameObject a, GameObject b)
    {
        return a.GetComponent<CardState>().cardType < b.GetComponent<CardState>().cardType ? -1 : 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        CardList = new List<GameObject>();

        var cardlist = Resources.LoadAll<GameObject>("Card");
        foreach (GameObject c in cardlist)
        {
            CardList.Add(c);
        }

        CardList.Sort(compare);

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
        gameData.CharLevel[0] = CharLevel[0];
        gameData.Char_1_Card = Card;
        gameData.Char_1_BaseStatus = BaseStatus;
        gameData.Char_1_BaseStatusPer = BaseStatusPer;
        gameData.Char_1_BaseStatusAdd = BaseStatusAdd;
    }

    private void LoadIngameData()
    {
        CharLevel[0] = gameData.CharLevel[0];
        Card = gameData.Char_1_Card;
        BaseStatus = gameData.Char_1_BaseStatus;
        BaseStatusPer = gameData.Char_1_BaseStatusPer;
        BaseStatusAdd = gameData.Char_1_BaseStatusAdd;
    }

    private void Char_1_SetStatus()
    {
        DefaultStatus[0] = BaseStatus[0] + CharLevel[0] * 12;
        DefaultStatus[1] = BaseStatus[1] + CharLevel[0] * 3;
        DefaultStatus[2] = BaseStatus[2] + CharLevel[0] * 10;
        DefaultStatus[3] = BaseStatus[3] + CharLevel[0] / 5 * 5;
        DefaultStatus[4] = BaseStatus[4] + CharLevel[0] / 5 * 10;

        StatusPer[0] = CharLevel[0] / 5 * 3 + ItemStatusPer[0];
        StatusPer[1] = CharLevel[0] / 5 * 3 + ItemStatusPer[1];
        StatusPer[2] = CharLevel[0] / 5 * 3 + ItemStatusPer[2];

        StatusAdd[0] = CharLevel[0] * 15 + ItemStatusAdd[0];
        StatusAdd[1] = CharLevel[0] * 5 + ItemStatusAdd[1];
        StatusAdd[2] = ItemStatusAdd[2];
        StatusAdd[3] = ItemStatusAdd[3];
        StatusAdd[4] = ItemStatusAdd[4];
    }



}

public class GameData
{
    public int[] CharLevel = new int[1];

    public GameObject[] Char_1_Card = new GameObject[4];

    public float[] Char_1_BaseStatus = new float[5];

    public float[] Char_1_BaseStatusPer = new float[3];

    public float[] Char_1_BaseStatusAdd = new float[5];
}

