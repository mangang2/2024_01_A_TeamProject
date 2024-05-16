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


    // Start is called before the first frame update
    void Start()
    {
        cardDrawing = false;
        Card[0] = FindObjectOfType<GameManager>().Card[0];
        Card[1] = FindObjectOfType<GameManager>().Card[1];
        Card[2] = FindObjectOfType<GameManager>().Card[2];
        Card[3] = FindObjectOfType<GameManager>().Card[3];
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


    private IEnumerator AddCard()
    {
        int cardType;
        float addCardCoolTime;
        cardType = Random.Range(0, 4);
 
        spawnCard = Card[cardType];
 
        Instantiate(spawnCard, transform);
        
        addCardCoolTime = 0.6f;
        while(addCardCoolTime >=0)
        {
            addCardCoolTime -= Time.deltaTime;
            yield return null;
        }

        cardDrawing = false;
    }
}
