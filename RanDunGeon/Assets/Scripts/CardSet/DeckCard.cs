using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DeckCard : MonoBehaviour
{
    public GameObject CardSelectCavas;
    public TextMeshProUGUI textBox;
    public Image SkillImange;
    public TextMeshProUGUI InfoText;

    [SerializeField]
    private int DeckNum;

    [SerializeField]
    private GameObject DeckManager;

    private GameManager GM;
    public GameObject CardType;

    // Start is called before the first frame update
    void Start()
    {
        LoadCard();
    }

    

    public void OnClick()
    {
        DeckManager.GetComponent<DeckManager>().NowSelecting = gameObject;
        CardSelectCavas.SetActive(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while(CardSelectCavas.GetComponent<CanvasGroup>().alpha < 1)
        {
            CardSelectCavas.GetComponent<CanvasGroup>().alpha += 3 * Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    public void NewCardSet()
    {
        if (CardType != null)
        {
            textBox.text = CardType.GetComponent<CardInfo>().NameText;
            SkillImange.sprite = CardType.GetComponent<CardState>().SkillSprite;
            SkillImange.enabled = true;
            InfoText.text = CardType.GetComponent<CardInfo>().InfoText;
        }
        else
        {
            textBox.text = "";
            SkillImange.enabled = false;
        }
    }

    private void LoadCard()
    {
        DeckManager = GameObject.Find("DeckManager");
        GM = GameManager.Instance;
        CardType = GM.GetComponent<GameManager>().Card[DeckNum - 1];

        if (CardType != null)
        {
            textBox.text = CardType.GetComponent<CardInfo>().NameText;
            InfoText.text = CardType.GetComponent<CardInfo>().InfoText;
        }
        else
        {
            textBox.text = "카드가 없어!";
        }

        SkillImange.sprite = CardType.GetComponent<CardState>().SkillSprite;
        SkillImange.enabled = true;
        CardSelectCavas.GetComponent<CanvasGroup>().alpha = 0;
    }
}
