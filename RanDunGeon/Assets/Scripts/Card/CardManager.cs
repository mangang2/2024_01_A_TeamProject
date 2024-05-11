using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private GameObject spawnCard;
    private int isCard;
    public bool cardDrawing = false;
    public bool doMerge = false;
    public bool stopDraw = false;

    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
    public GameObject Card4;

    // Start is called before the first frame update
    void Start()
    {
        cardDrawing = false;
    }

    // Update is called once per frame
    void Update()
    {
        isCard = transform.childCount;

        if(isCard < 5 && cardDrawing == false && doMerge == false)
        {
            stopDraw = false;
            cardDrawing = true;
            StartCoroutine(AddCard());
        }
        if(isCard == 5)
        {
            stopDraw = true;
        }
    }


    private IEnumerator AddCard()
    {
        int cardType;
        float addCardCoolTime;
        cardType = Random.Range(1, 5);
 
        if(cardType == 1 ) { spawnCard = Card1; }
        else if(cardType == 2 ) { spawnCard = Card2;}
        else if(cardType == 3 ) { spawnCard = Card3;}
        else if(cardType == 4 ) { spawnCard = Card4;}
 
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
