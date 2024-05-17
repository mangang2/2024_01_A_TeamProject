using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectButton : MonoBehaviour
{
    public GameObject CardType;

    private GameObject DeckManager;

    private GameObject CardSelectCavas;

    private float holdTime;

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private bool Clicking,notClick = false;

    // Start is called before the first frame update
    void Start()
    {
        DeckManager = GameObject.Find("DeckManager");
        CardSelectCavas = GameObject.Find("CardSelect");
        nameText.text = CardType.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(Clicking == true)
        {
            holdTime += Time.deltaTime;
        }

        if(holdTime > 1f && Clicking == true)
        {
            notClick = true;
            Debug.Log("카드 설명 표시");
            Clicking = false;
        }

    }

    public void ClickDown()
    {
        holdTime = 0;
        Clicking = true;
        notClick = false;
    }

    public void ClickUp()
    {
        Clicking = false;

        if(notClick == false && CardType.GetComponent<CardState>().Unlock == true)           //Click
        {
            DeckManager.GetComponent<DeckManager>().CardSet(CardType);
            StartCoroutine(FadeOut());
        }
        else if(CardType.GetComponent<CardState>().Unlock == false)
        {
            Debug.Log("아직 해금 되지 않은 카드야!");
        }
    }

    private IEnumerator FadeOut()
    {

        while (CardSelectCavas.GetComponent<CanvasGroup>().alpha >= 0)
        {
            CardSelectCavas.GetComponent<CanvasGroup>().alpha -= 3 * Time.deltaTime;

            if(CardSelectCavas.GetComponent<CanvasGroup>().alpha <= 0)
            {
                CardSelectCavas.SetActive(false);
            }

            yield return null;
        }
        yield break;
    }
}
