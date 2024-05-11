using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSceneMove : MonoBehaviour
{
    public bool CardHold = false;
    
    private bool memoryPosition = false;
    private bool changePosition = false;
    private Vector3 bfPosition, newPosition;
    private GameObject deckSetter;


    // Start is called before the first frame update
    void Start()
    {
        bfPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardDrag()
    {
        if (memoryPosition == false)
        {
            bfPosition = transform.position;
            memoryPosition = true;
        }

        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        CardHold = true;
        transform.position = new Vector3(MousePosition.x, MousePosition.y, -5);
    }

    public void CardDrop()
    {
        CardHold = false;
        if (changePosition == false)
        {
            transform.position = bfPosition;
            if(deckSetter.GetComponent<DeckSet>().BfCard != null)
            {
                Debug.Log("카드있음");
                Destroy(deckSetter.GetComponent<DeckSet>().temp);
                deckSetter.GetComponent<DeckSet>().BfCard = this.gameObject;
            }
        }
        else
        {
            transform.position = new Vector3(newPosition.x, newPosition.y, bfPosition.z);
            bfPosition = transform.position;
        }

        memoryPosition = false;
    }

    private void OnTriggerEnter(Collider deckSet)
    {
        if (deckSet.transform.tag == "DeckSet" && CardHold == true)
        {
            deckSetter = deckSet.gameObject;
            Debug.Log("newStop");
            changePosition = true;
            newPosition = deckSet.transform.position;
        }
    }

    private void OnTriggerExit(Collider outDeckSet)
    {
        changePosition = false;
    }
}
