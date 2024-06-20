using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStoneCard : MonoBehaviour
{
    private GameObject TurnManager;

    private GameObject player;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        TurnManager = GameObject.FindGameObjectWithTag("TurnManager");
    }

    // Update is called once per frame
    void Update()
    {
        float playerAd;
        float enemyDf;
        float enemyDd;
        float finalDamage;
        float criP;
        float criD;
        float ED;

        int CardRank;

        float DamageRank = 1.6f;

        if (GetComponent<CardState>().skillUse == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;
            criP = player.GetComponent<CharacterStatus>().CriPercent;
            ED = player.GetComponent<CharacterStatus>().EnhanceDamage * 0.01f;


            if (CardRank == 1) DamageRank = 1.6f;
            if (CardRank == 2) DamageRank = 1.8f;
            if (CardRank == 3) DamageRank = 2.0f;


            enemyDf = enemy.GetComponent<CharacterStatus>().Defense;
            enemyDd = enemy.GetComponent<CharacterStatus>().DownDamage;

            if (Random.Range(0f, 100f) <= criP)
            {
                criD = (100 + player.GetComponent<CharacterStatus>().CriDamage) * 0.01f;
                Debug.Log("치명타!");
            }
            else
            {
                criD = 1f;
            }


            finalDamage = playerAd * DamageRank * enemyDf * criD * ED - enemyDd;

            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage;
            Debug.Log(finalDamage.ToString("F0") + "의 물리피해를 입힙니다.");
            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject,0.3f);
            enabled = false;
        }
    }
}
