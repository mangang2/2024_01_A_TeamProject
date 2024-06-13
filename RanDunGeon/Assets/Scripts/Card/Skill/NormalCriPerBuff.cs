using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCriPerBuff : MonoBehaviour
{
    private GameObject TurnManager;

    private GameObject player;
    private int bonusTurn = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TurnManager = GameObject.FindGameObjectWithTag("TurnManager");
    }

    // Update is called once per frame
    void Update()
    {
        float playerCriPBuff;

        float CriPBuff = 0;
        float CriDBuff = 0;

        int turn = 2;

        int CardRank;

        if (GetComponent<CardState>().skillUse == true)
        {
            playerCriPBuff = player.GetComponent<CharacterStatus>().CriPercentBuff;
           
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1)
            {
                CriPBuff = 10;
                CriDBuff = 15;
                bonusTurn = 0;
                TurnManager.GetComponent<TurnManager>().PWorkCount--;
            }
            if (CardRank == 2)
            {
                CriPBuff = 15;
                CriDBuff = 25;
                bonusTurn = 0;
                TurnManager.GetComponent<TurnManager>().PWorkCount--;
            }
            if (CardRank == 3)
            {
                CriPBuff = 25;
                CriDBuff = 50;
                bonusTurn = 1;
            }

            if (playerCriPBuff < CriPBuff)
            {
                player.GetComponent<CharacterStatus>().CriPercentBuff = CriPBuff;
                player.GetComponent<CharacterStatus>().CriDamageBuff = CriDBuff;
                player.GetComponent<CharacterStatus>().CriPercentBuffTrun = turn;
                player.GetComponent<CharacterStatus>().CriDamageBuffTrun = turn;
                Debug.Log("행동 횟수를 " + bonusTurn + "회 추가하고, 이번 1턴동안 치명타 확률이 " + CriPBuff + "%, 치명타 피해가 " + CriDBuff + "% 증가합니다.");
            }
            else if (playerCriPBuff == CriPBuff)
            {
                Debug.Log("이미 동일한 효과가 적용중입니다.");
            }
            else
            {
                Debug.Log("현재 더 강력한 효과가 적용중입니다.");
            }


            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject,0.3f);
            enabled = false;
        }
    }
}
