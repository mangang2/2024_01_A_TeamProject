using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDamageDown : MonoBehaviour
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
        int playerDd;
        int Dd = 5;
        int DdTurn = 1;
        int CardRank;

        if (GetComponent<CardState>().skill == true)
        {
            playerDd = player.GetComponent<CharacterStatus>().DownDamage;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1) Dd = 5;
            if (CardRank == 2) Dd = 8;
            if (CardRank == 3) Dd = 15;

            if(playerDd < Dd)
            {
                player.GetComponent<CharacterStatus>().DownDamage = Dd;
                player.GetComponent<CharacterStatus>().DownDamageTurn = DdTurn;
                Debug.Log("1턴 동안 받는 피해가 " + Dd + "만큼 감소합니다.");
            }
            else if(playerDd == Dd)
            {
                player.GetComponent<CharacterStatus>().DownDamage = Dd;
                player.GetComponent<CharacterStatus>().DownDamageTurn = DdTurn;
                Debug.Log("이미 동일한 효과가 적용중입니다.");
            }
            else
            {
                Debug.Log("현재 더 강력한 효과가 적용중입니다.");
            }

            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skill = false;
            Destroy(gameObject);
        }
    }
}
