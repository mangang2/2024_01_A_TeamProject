using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListLoad : MonoBehaviour
{
    public GameObject CardListPrefabs,CardListNull;

    public float LastCardPosY;

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
            temp.transform.SetParent(gameObject.transform);
            temp.GetComponent<CardSelectButton>().CardType = CardList[i - 1];
            temp.GetComponent<CardSelectButton>().SendPosY();
        }

        for(int o = 1; o <= addNum; o++)
        {
            GameObject temp = Instantiate(CardListNull);
            temp.transform.SetParent(gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            transform.Translate(Vector3.up * Input.GetAxis("Mouse ScrollWheel") * -500);
        }

        if(transform.localPosition.y < 430)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, 430);
        }

        if (transform.localPosition.y > LastCardPosY + 1030)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, LastCardPosY + 1030);
        }
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, 430);
    }
}
