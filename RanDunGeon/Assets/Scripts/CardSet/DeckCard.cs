using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DeckCard : MonoBehaviour
{
    public GameObject CardSelectCavas;
    public Text textBox;

    [SerializeField]
    private int DeckNum;

    [SerializeField]
    private GameObject DeckManager;

    private GameManager GM;
    public GameObject CardType;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadCard", 0.2f);
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
            textBox.text = CardType.name.ToString();
        }
        else
        {
            textBox.text = "null";
        }
    }

    private void LoadCard()
    {
        DeckManager = GameObject.Find("DeckManager");
        GM = GameManager.Instance;
        CardType = GM.GetComponent<GameManager>().Card[DeckNum - 1];

        if (CardType.GetComponent<CardState>().Unlock == false)
        {
            CardType = null;
        }

        if (CardType != null)
        {
            textBox.text = CardType.name.ToString();
        }
        else
        {
            textBox.text = "null";
        }
        CardSelectCavas.GetComponent<CanvasGroup>().alpha = 0;
    }
}
