using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("사용할 카드")]
    public GameObject[] Card = new GameObject[4];

    [Header("기초 스테이터스")]
    public float[] DefaultStatus = new float[5];

    [Header("스테이터스 % 추가")]
    public float[] StatusPer = new float[3];

    [Header("스테이터스 정수값 추가")]
    public float[] StatusAdd = new float[5];

    public List<GameObject> CardList = new List<GameObject>();

    public List<bool> UnlockCard = new List<bool>();

    public static GameManager Instance { get; private set; }

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
