using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject NowSelecting;

    public GameObject CardDeck1, CardDeck2, CardDeck3, CardDeck4;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardSet(GameObject NewCardType)
    {
        if(CardDeck1.GetComponent<DeckCard>().CardType == NewCardType)
        {
            CardDeck1.GetComponent<DeckCard>().CardType = null;
            CardDeck1.GetComponent<DeckCard>().NewCardSet();
        }

        if (CardDeck2.GetComponent<DeckCard>().CardType == NewCardType)
        {
            CardDeck2.GetComponent<DeckCard>().CardType = null;
            CardDeck2.GetComponent<DeckCard>().NewCardSet();
        }

        if (CardDeck3.GetComponent<DeckCard>().CardType == NewCardType)
        {
            CardDeck3.GetComponent<DeckCard>().CardType = null;
            CardDeck3.GetComponent<DeckCard>().NewCardSet();
        }

        if (CardDeck4.GetComponent<DeckCard>().CardType == NewCardType)
        {
            CardDeck4.GetComponent<DeckCard>().CardType = null;
            CardDeck4.GetComponent<DeckCard>().NewCardSet();
        }

        NowSelecting.GetComponent<DeckCard>().CardType = NewCardType;
        NowSelecting.GetComponent<DeckCard>().NewCardSet();
    }

    public void SaveDeck()
    {
        if (CardDeck1.GetComponent<DeckCard>().CardType != null && CardDeck2.GetComponent<DeckCard>().CardType != null && CardDeck3.GetComponent<DeckCard>().CardType != null && CardDeck4.GetComponent<DeckCard>().CardType != null)
        {
            GM.Card[0] = CardDeck1.GetComponent<DeckCard>().CardType;
            GM.Card[1] = CardDeck2.GetComponent<DeckCard>().CardType;
            GM.Card[2] = CardDeck3.GetComponent<DeckCard>().CardType;
            GM.Card[3] = CardDeck4.GetComponent<DeckCard>().CardType;
            //GM.SaveData();
            Debug.Log("카드 저장");
        }
        else
        {
            Debug.Log("4장을 모두 선택해줘!");
        }
    }
}
