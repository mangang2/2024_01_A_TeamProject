using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject NowSelecting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardSet(GameObject NewCardType)
    {
        NowSelecting.GetComponent<DeckCard>().CardType = NewCardType;
        NowSelecting.GetComponent<DeckCard>().NewCardSet();
    }
}
