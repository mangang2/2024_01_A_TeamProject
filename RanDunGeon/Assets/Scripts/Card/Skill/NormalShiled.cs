using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShield : MonoBehaviour
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
        float playerDf = 0;
        float playerShield = 0;
        float shieldRank = 0;
        int shieldTurn = 3;
        float shield = 0;
        int CardRank;

        if (GetComponent<CardState>().skillUse == true)
        {
            playerDf = player.GetComponent<CharacterStatus>().LastDefense;
            playerShield = player.GetComponent<CharacterStatus>().Shield;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1) shieldRank = 1.5f;
            if (CardRank == 2) shieldRank = 2.2f;
            if (CardRank == 3) shieldRank = 4f;

            shield = playerDf * shieldRank;

            if (playerShield < shield)
            {
                player.GetComponent<CharacterStatus>().ShieldTurn = shieldTurn;
                player.GetComponent<CharacterStatus>().Shield = shield;
                player.GetComponent<CharacterStatus>().MaxShield = shield;
                Debug.Log("3턴 동안 유지되는 " + shield + "만큼의 쉴드를 얻습니다.");
            }
            else if (playerShield == shield)
            {
                player.GetComponent<CharacterStatus>().ShieldTurn = shieldTurn;
                player.GetComponent<CharacterStatus>().Shield = shield;
                player.GetComponent<CharacterStatus>().MaxShield = shield;
            }
            else
            {
                player.GetComponent<CharacterStatus>().ShieldTurn = shieldTurn;
                Debug.Log("현재 더 강력한 효과가 적용중입니다.");
            }

            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject, 0.3f);
            enabled = false;
        }
    }
}
