using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardSelectButton : MonoBehaviour
{
    public GameObject CardListLoader,CardInfoUI;

    public GameObject CardType;

    private GameObject DeckManager;

    private GameObject CardSelectCavas;

    private float holdTime;

    [SerializeField]
    private Button CardButton;

    [SerializeField]
    private Image skillImage;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private bool Clicking,notClick = false;

    private void Awake()
    {
        CardListLoader = GameObject.Find("CardLayout");
    }

    // Start is called before the first frame update
    void Start()
    {

        DeckManager = GameObject.Find("DeckManager");
        CardSelectCavas = GameObject.Find("CardSelect");
        nameText.text = CardType.GetComponent<CardInfo>().NameText;
        skillImage.sprite = CardType.GetComponent<CardState>().SkillSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (CardType.GetComponent<CardState>().Unlock == false)
        {
            CardButton.interactable = false;
        }
        else
        {
            CardButton.interactable = true;
        }
        if (Clicking == true)
        {
            holdTime += Time.deltaTime;
        }

        if(holdTime > 0.3f && Clicking == true)
        {
            notClick = true;
            CardInfoUI.SetActive(true);
            CardInfoUI.GetComponent<CardInfoUI>().UIOn(CardType.GetComponent<CardState>().SkillSprite, CardType.GetComponent<CardInfo>().NameText, CardType.GetComponent<CardInfo>().InfoText);
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
        else if(CardType.GetComponent<CardState>().Unlock == false && holdTime <= 0.3f)
        {
            Debug.Log("아직 해금 되지 않은 카드야!");
        }
    }

    public void SendPosY()
    {
        CardListLoader.GetComponent<CardListLoad>().LastCardPosY = transform.position.y;
    }

    private IEnumerator FadeOut()
    {

        while (CardSelectCavas.GetComponent<CanvasGroup>().alpha > 0)
        {
            CardSelectCavas.GetComponent<CanvasGroup>().alpha -= 3 * Time.deltaTime;
            yield return null;
        }

        CardSelectCavas.SetActive(false);
        yield break;
    }
}
