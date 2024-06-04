using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRecoveryCard : MonoBehaviour
{
    private GameObject TurnManager;

    private GameObject player;

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

        if (GetComponent<CardState>().skill == true)
        {
            playerMaxHp = player.GetComponent<CharacterStatus>().MaxHp;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1) recoveryRank = 0.25f;
            if (CardRank == 2) recoveryRank = 0.3f;
            if (CardRank == 3) recoveryRank = 0.4f;

            finalHeal = playerMaxHp * recoveryRank;

            player.GetComponent<CharacterStatus>().Recover += finalHeal;

            Debug.Log("체력을 " + finalHeal.ToString("F0") + "만큼 회복합니다.");

            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skill = false;
            Destroy(gameObject,0.3f);
            enabled = false;
        }
    }
}
