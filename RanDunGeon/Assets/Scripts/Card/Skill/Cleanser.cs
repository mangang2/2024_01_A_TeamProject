using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanser : MonoBehaviour
{

    private GameObject TurnManager;

    private GameObject player;

    private int[] DebuffTurn = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        TurnManager = GameObject.FindGameObjectWithTag("TurnManager");
    }

    // Update is called once per frame
    void Update()
    {
        float playerMaxHp;
        float recoveryRank = 0.05f;
        float finalHeal;
        int CardRank;
        int ClearCount = 0;

        if (GetComponent<CardState>().skillUse == true)
        {
            playerMaxHp = player.GetComponent<CharacterStatus>().MaxHp;
            CardRank = GetComponent<CardState>().cardRank;
            DebuffTurn[0] = player.GetComponent<CharacterStatus>().AdDebuffTurn;
            DebuffTurn[1] = player.GetComponent<CharacterStatus>().DefenseDebuffTurn;
            DebuffTurn[2] = player.GetComponent<CharacterStatus>().CriPercentDebuffTrun;
            DebuffTurn[3] = player.GetComponent<CharacterStatus>().CriDamageDebuffTrun;
            DebuffTurn[4] = player.GetComponent<CharacterStatus>().EnhanceDDebuffTurn;

            for (int i = 0; i < DebuffTurn.Length; i++)
            {
                if (DebuffTurn[i] > 0)
                {
                    ClearCount++;
                }
            }

            if (CardRank == 1) recoveryRank = 0.025f;
            if (CardRank == 2) recoveryRank = 0.07f;
            if (CardRank == 3) recoveryRank = 0.1f;

            player.GetComponent<CharacterStatus>().AdDebuffTurn = 0;
            player.GetComponent<CharacterStatus>().DefenseDebuffTurn = 0;
            player.GetComponent<CharacterStatus>().CriPercentDebuffTrun = 0;
            player.GetComponent<CharacterStatus>().CriDamageDebuffTrun = 0;
            player.GetComponent<CharacterStatus>().EnhanceDDebuffTurn = 0;



            finalHeal = playerMaxHp * recoveryRank * ClearCount;

            player.GetComponent<CharacterStatus>().Recover += finalHeal;

            Debug.Log("모든 디버프를 해제하고, 체력을 " + finalHeal.ToString("F0") + "만큼 회복합니다.");

            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject, 0.3f);
            enabled = false;
        }
    }
}
