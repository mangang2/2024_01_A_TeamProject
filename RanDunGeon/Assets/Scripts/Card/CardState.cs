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
    GameObject target1;
    GameObject target2;

    public bool DoMerge = false;
    private bool DoMove = false;

    public int cardType;        //1 = 공격 2 = 방어 3 = 회복 4 = 짱돌
    public int cardRank = 1;
    public bool skill = false;

    public TextMeshPro rankText;

    private string infoText;

    RaycastHit stopTarget;
    RaycastHit hit;


    private void Awake()
    {
        infoText = gameObject.GetComponent<CardInfo>().InfoText;
    }

    // Start is called before the first frame update
    void Start()
    {
        rankText.text = cardRank.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        

        if (DoMove == false)
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
            transform.DOMoveY(transform.position.y + 3, 0.5f);
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
