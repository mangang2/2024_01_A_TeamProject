using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListLoad : MonoBehaviour
{
    public GameObject CardListPrefabs,CardListNull;

    private GameManager GM;

    private List<GameObject> CardList;

    private int listLenght;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        CardList = GM.GetComponent<GameManager>().CardList;
        listLenght = CardList.Count;

        int addNum = 5 - (listLenght % 5);

        for(int i = 1; i <= listLenght; i++)
        {
            GameObject temp = Instantiate(CardListPrefabs);
            temp.transform.parent = gameObject.transform;
            temp.GetComponent<CardSelectButton>().CardType = CardList[i - 1];
        }

        for(int o = 1; o <= addNum; o++)
        {
            GameObject temp = Instantiate(CardListNull);
            temp.transform.parent = gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
