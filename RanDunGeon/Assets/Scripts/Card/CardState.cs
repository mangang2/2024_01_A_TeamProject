using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TreeEditor;
using UnityEngine.UIElements;
using DG.Tweening;

public class CardState : MonoBehaviour
{
    public float moveSpeed;
    private GameObject target1;
    private GameObject target2;
    private GameObject CardManager;

    public bool Unlock = false;

    public bool DoMerge = false;
    private bool DoMove = false;

    public int cardType;
    public int cardRank = 1;
    public int cardRare;
    public bool skill = false;

    public TextMeshPro rankText;

    private Material Mt;

    private string infoText;

    private RaycastHit stopTarget;
    private RaycastHit hit;

    private Color FadeOut;

    private void Awake()
    {
        CardManager = GameObject.Find("CardManager");
        infoText = gameObject.GetComponent<CardInfo>().InfoText;
    }

    // Start is called before the first frame update
    void Start()
    {
        rankText.text = cardRank.ToString();
        Mt = GetComponent<MeshRenderer>().material;
        FadeOut = new Color(Mt.color.r,Mt.color.g,Mt.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {

        

        if (DoMove == false && CardManager.GetComponent<CardManager>().UsingCard == false)
        {
            if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out hit, 3))            //합쳐질 카드 인식
            {
                target2 = hit.collider.gameObject;
                if (target2.transform.tag == "Card" && cardRank <= 2) //&& cardType != 4)
                {
                    if (target2.GetComponent<CardState>().cardType == cardType && target2.GetComponent<CardState>().cardRank == cardRank)
                    {
                        MergeCard();
                    }
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, new Vector3(-1, 0, 0), out stopTarget, 40))        //카드 정렬
                {
                    DoMove = true;
                    target1 = stopTarget.collider.gameObject;
                    CardMove();
                }
            }
        }
        
        if(skill == true)
        {
            CardManager.GetComponent<CardManager>().UsingCard = true;
            transform.DOMoveY(5, 0.3f);
            Mt.DOColor(FadeOut,0.3f);
        }
        
    }


    private void CardMove()
    {
        float stopPositionX = target1.transform.position.x + 4f;
        transform.DOMoveX(stopPositionX, 0.3f).OnComplete(DoMoveTurn);
    }

    private void MergeCard()
    {
        GetComponent<Collider>().isTrigger = true;
        transform.Translate(new Vector3 (0,0,0.1f));
        if (DoMerge == false)
        {
            DoMerge = true;
            target2.GetComponent<CardState>().DoMerge = true;
            transform.DOMoveX(target2.transform.position.x, 0.3f).OnComplete(DoMergeTurn);

        }
    }

    public void RankUp()
    {
        DoMerge = false;
        cardRank += 1;
            rankText.text = cardRank.ToString();
    }

    private void DoMoveTurn()
    {
        DoMove = false;
    }

    private void DoMergeTurn()
    {
        target2.GetComponent<CardState>().RankUp();
        DoMerge = false;
        Destroy(gameObject);
    }

    public void CardInfo()
    {
        Debug.Log("카드 스킬 : " + infoText);
    }

    
}
