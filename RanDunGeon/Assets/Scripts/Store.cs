using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Store : MonoBehaviour
{
    public Button[] StoreCardButton = new Button[3];
    public TextMeshProUGUI[] StoreNameText, PriceText = new TextMeshProUGUI[3];
    public Image[] SkillImage = new Image[3];

    public TextMeshProUGUI NameText, InfoText;

    public GameObject CheckUI;

    private GameManager GM;

    [SerializeField]
    private List<GameObject> cardList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> ableCard = new List<GameObject>();

    private int cardCount;

    private GameObject[] nowStoreCard = new GameObject[3];

    private int selectCard = 1;
    // Start is called before the first frame update
    void Start()
    { 
        GM = GameManager.Instance;

        cardList = GM.CardList;
        if (GM.BeStoreCard == true)
        {
            LoadCard();
        }
        else
        {
            addCard();
        }
        StopAllCoroutines();
        StartCoroutine(typingMotion("사장님", "어서오게나"));
    }

    private void LoadCard()
    {
        ableCard.Clear();
        cardCount = 0;
        foreach (GameObject c in cardList)
        {
            if (c.GetComponent<CardState>().Unlock == false && c.GetComponent<CardState>().cardRare != 0)
            {
                ableCard.Add(c);
            }
        }
        for (int i = 0; i < 3; i++)
        {
            nowStoreCard[i] = GM.StoreCard[i];
            StoreNameText[cardCount].text = nowStoreCard[i].GetComponent<CardInfo>().NameText;
            SkillImage[cardCount].sprite = nowStoreCard[i].GetComponent<CardState>().SkillSprite;

            int price = checkPrice(nowStoreCard[i].GetComponent<CardState>().cardRare) / 1000;
            if (nowStoreCard[i].GetComponent<CardState>().Unlock == true)
            {
                PriceText[cardCount].text = "품절";
            }
            else
            {
                PriceText[cardCount].text = price.ToString() + ",000G";
            }
            ableCard.Remove(nowStoreCard[i]);
            cardCount++;
        }
    }

    private void addCard()
    {
        ableCard.Clear();
        cardCount = 0;
        foreach (GameObject c in cardList)
        {
            if (c.GetComponent<CardState>().Unlock == false && c.GetComponent<CardState>().cardRare != 0)
            {    
                ableCard.Add(c);
            }
        }

        int countTemp;

        if (ableCard.Count >= 3)
            countTemp = 3;
        else
            countTemp = ableCard.Count;
       
        for (int i = 0; i < 3; i++)
        {
            if (i < countTemp)
            {
                nowStoreCard[i] = ableCard[Random.Range(0, ableCard.Count)];
                StoreNameText[cardCount].text = nowStoreCard[i].GetComponent<CardInfo>().NameText;
                SkillImage[cardCount].sprite = nowStoreCard[i].GetComponent<CardState>().SkillSprite;

                int price = checkPrice(nowStoreCard[i].GetComponent<CardState>().cardRare) / 1000;
                PriceText[cardCount].text = price.ToString() + ",000G";
                ableCard.Remove(nowStoreCard[i]);
            }
            else
            {
                PriceText[cardCount].text = "품절";
                StoreNameText[cardCount].text = "";
                nowStoreCard[i] = null;
            }
            cardCount++;
        }
        
        GM.StoreCard = nowStoreCard;
        GM.BeStoreCard = true;
    }

    private int checkPrice(int rare)
    {
        switch(rare)
        {
            case 1:
                return 5000;
            case 2:
                return 20000;
            case 3:
                return 40000;
            default:
                return 0;
        }
    }

    public void SelectCard1(int num)
    {
        StopAllCoroutines();
        selectCard = num;
        if(num == -1)
        {
            switch(Random.Range(0,5))
            {
                case 0:
                    StartCoroutine(typingMotion("사장님", "혹시 찾는 물건이라도 있나?"));
                    break;
                case 1:
                    StartCoroutine(typingMotion("사장님", "나도 한땐 자네같이 모험을 다니곤 했지 지금 생각해보면 미친짓이였네"));
                    break;
                case 2:
                    StartCoroutine(typingMotion("사장님", "항상 좋은 물건만 있지만 오늘은 더욱 좋은 녀석들일세"));
                    break;
                case 3:
                    StartCoroutine(typingMotion("사장님", "자네 물건을 살 생각은 있는거겠지?"));
                    break;
                case 4:
                    StartCoroutine(typingMotion("사장님", "행상인도 나름 힘든 직업이라네 내 덕분에 자네가 편하게 카드를 구매할 수 있는거지"));
                    break;
            }
        }
        else if(nowStoreCard[num] != null)
        {
            if (nowStoreCard[num].GetComponent<CardState>().Unlock == true)
            {
                switch (Random.Range(0, 3))
                {
                    case 0:
                        StartCoroutine(typingMotion("사장님", "그 상품은 품절이라네"));
                        break;
                    case 1:
                        StartCoroutine(typingMotion("사장님", "이미 구매한 물건 아닌가"));
                        break;
                    case 2:
                        StartCoroutine(typingMotion("사장님", "진열대에서 치워야겠군"));
                        break;
                }
            }
            else
            StartCoroutine(typingMotion(nowStoreCard[num].GetComponent<CardInfo>().NameText, nowStoreCard[num].GetComponent<CardInfo>().InfoText));
        }
        else
        {
            StartCoroutine(typingMotion("사장님", "새로 물건을 들여와야겠군"));
        }
    }

    public void BuyCard()
    {
        if (GM.Gold >= checkPrice(nowStoreCard[selectCard].GetComponent<CardState>().cardRare) && nowStoreCard[selectCard].GetComponent<CardState>().Unlock == false)
        {
            GM.Gold -= checkPrice(nowStoreCard[selectCard].GetComponent<CardState>().cardRare);
            nowStoreCard[selectCard].GetComponent<CardState>().Unlock = true;
            PriceText[selectCard].text = "품절";
            StopAllCoroutines();
            switch (Random.Range(0, 3))
            {
                case 0:
                    StartCoroutine(typingMotion("사장님", "항상 고맙다네"));
                    break;
                case 1:
                    StartCoroutine(typingMotion("사장님", "혹시 더 필요한건 없나?"));
                    break;
                case 2:
                    StartCoroutine(typingMotion("사장님", "보는눈이 있구만 그래"));
                    break;
            }
        }
        else
        {
            int temp = selectCard;
            StopAllCoroutines();
            switch (Random.Range(0, 3))
            {
                case 0:
                    StartCoroutine(typingMotion("사장님", "자네 돈은 있는겐가?"));
                    break;
                case 1:
                    StartCoroutine(typingMotion("사장님", "먼저 돈부터 주시게"));
                    break;
                case 2:
                    StartCoroutine(typingMotion("사장님", "가격은 제대로 본거겠지?"));
                    break;
            }
            selectCard = temp;
        }
    }

    public void ResetCheck()
    {
        CheckUI.SetActive(true);
    }

    public void NewCard()
    {
        if (GM.Gold >= 10000)
        {
            if (ableCard.Count > 0)
            {
                GM.Gold -= 10000;
                addCard();
                StopAllCoroutines();
                switch (Random.Range(0, 3))
                {
                    case 0:
                        StartCoroutine(typingMotion("사장님", "새 물건을 들여왔네"));
                        break;
                    case 1:
                        StartCoroutine(typingMotion("사장님", "물건이 마음에 안든겐가?"));
                        break;
                    case 2:
                        StartCoroutine(typingMotion("사장님", "이번에도 좋은 물건이 많이 있네"));
                        break;
                }
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(typingMotion("사장님", "더 들여올 물건이 이젠 없네"));
            }

            CheckUI.SetActive(false);
        }
        else
        {
            StopAllCoroutines();
            switch (Random.Range(0, 3))
            {
                case 0:
                    StartCoroutine(typingMotion("사장님", "자네 돈은 있는겐가?"));
                    break;
                case 1:
                    StartCoroutine(typingMotion("사장님", "먼저 돈부터 주시게"));
                    break;
                case 2:
                    StartCoroutine(typingMotion("사장님", "가격은 제대로 본거겠지?"));
                    break;
            }
            CheckUI.SetActive(false);
        }
        
    }

    private IEnumerator typingMotion(string name,string info)
    {
        if(name == "사장님")
        {
            selectCard = -1;
        }
        NameText.text = "";
        InfoText.text = "";
        for(int i =0; i < name.Length; i ++)
        {
            NameText.text += name[i];
            yield return new WaitForSeconds(0.02f);
        }

        for (int i = 0; i < info.Length; i++)
        {
            InfoText.text += info[i];
            yield return new WaitForSeconds(0.015f);
        }
        yield break;
    }

}
