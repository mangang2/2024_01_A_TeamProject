using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject TurnManager;
    public bool cardDrawing = false;
    public bool ClickAble = false;
    public bool doMerge = false;
    public bool stopDraw = false;
    public bool UsingCard = false;

    private GameObject[] Card = new GameObject[4];
    private GameObject spawnCard;
    private int isCard;
    private float checkTime, checkTime2;

    [SerializeField]
    private int[] nowCount = new int[4];


    // Start is called before the first frame update
    void Start()
    {
        cardDrawing = true;
        Invoke("LoadCard", 0.5f);
        nowCount = new int[] { 0, 0, 0, 0 };
    }

    // Update is called once per frame
    void Update()
    {
        isCard = transform.childCount;

        if(isCard < 5 && cardDrawing == false && doMerge == false && TurnManager.GetComponent<TurnManager>().pTurn == true)
        {
            stopDraw = false;
            cardDrawing = true;
            StartCoroutine(AddCard());
        }
        if(isCard == 5)
        {
            stopDraw = true;
        }

        if(cardDrawing == true)
        {
            ClickAble = false;
            checkTime = 0.4f;
        }

        if (cardDrawing == false && ClickAble == false)
        {
            checkTime -= Time.deltaTime;
        }

        if(checkTime <= 0)
        {
            ClickAble = true;
        }

        if(UsingCard)
        {
            checkTime2 -= Time.deltaTime;
        }
        else
        {
            checkTime2 = 0.3f;
        }

        if(checkTime2 <= 0)
        {
            UsingCard = false;
        }
       

    }


    private void LoadCard()
    {
        Card[0] = FindObjectOfType<GameManager>().Card[0];
        Card[1] = FindObjectOfType<GameManager>().Card[1];
        Card[2] = FindObjectOfType<GameManager>().Card[2];
        Card[3] = FindObjectOfType<GameManager>().Card[3];
        cardDrawing = false;
    }

    private int checkCount(GameObject card)
    {
        switch(card.GetComponent<CardState>().cardRank)
        {
            case 1:
                return 1;
            case 2:
                return 2;
            case 3:
                return 4;
            default:
                return 0;
        }
    }

    private int checkCard(GameObject card)
    {
        if(card.GetComponent<CardState>().cardType == Card[0].GetComponent<CardState>().cardType)
        {
            return 0;
        }
        else if (card.GetComponent<CardState>().cardType == Card[1].GetComponent<CardState>().cardType)
        {
            return 1;
        }
        else if (card.GetComponent<CardState>().cardType == Card[2].GetComponent<CardState>().cardType)
        {
            return 2;
        }
        else if (card.GetComponent<CardState>().cardType == Card[3].GetComponent<CardState>().cardType)
        {
            return 3;
        }
        return 0;
    }

    private IEnumerator AddCard()
    {
        AudioManager.instance.PlaySound("CardDraw");
        int cardType;
        float addCardCoolTime;

        nowCount = new int[] { 0, 0, 0, 0 };

        for (int i = 0; i < isCard; i++)
        {
            nowCount[checkCard(transform.GetChild(i).gameObject)] += checkCount(transform.GetChild(i).gameObject);
        }

        cardType = Random.Range(0, 4);

        if (nowCount[checkCard(Card[cardType])] < 5)
        {
            spawnCard = Card[cardType];
            Instantiate(spawnCard, transform);
        }
        else
        {
            StartCoroutine(AddCard());
        }
        
        addCardCoolTime = 0.3f;
        while(addCardCoolTime >=0)
        {
            addCardCoolTime -= Time.deltaTime;
            yield return null;
        }

        cardDrawing = false;
    }
}
