using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckSet : MonoBehaviour
{
    public GameObject GameManager, NewCard, BfCard, temp;
    public int SetNum;

    private void Awake()
    {
        switch(SetNum)
        {
            case 1:
                BfCard = GameManager.GetComponent<GameManager>().Card1;
                break;
            case 2:
                BfCard = GameManager.GetComponent<GameManager>().Card2;
                break;
            case 3:
                BfCard = GameManager.GetComponent<GameManager>().Card3;
                break;
            case 4:
                BfCard = GameManager.GetComponent<GameManager>().Card4;
                break;
            default:
                Debug.Log("제대로 지정해줘..!");
                break;

        }


        temp = Instantiate(BfCard,new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        temp.GetComponent<CardState>().enabled = false;
        temp.transform.localScale = new Vector3(9.600669f, 14.401f, 3.200223f);
        

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
