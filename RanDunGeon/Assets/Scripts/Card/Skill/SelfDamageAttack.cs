using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDamageAttack : MonoBehaviour
{
    private GameObject TurnManager;

    private GameObject player;
    private GameObject enemy;

    private float finalDamage2;
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
        float playerHp;
        float DisHp = 0;
        float enemyDf;
        float enemyDd;
        float finalDamage1;
        float criP;
        float criD;
        float ED;

        int CardRank;

        float DamageRank = 1.25f;
        float HpDamageBuffRank = 0.5f;

        if (GetComponent<CardState>().skillUse == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;
            criP = player.GetComponent<CharacterStatus>().CriPercent;
            ED = player.GetComponent<CharacterStatus>().EnhanceDamage * 0.01f;


            if (CardRank == 1)
            {
                DamageRank = 1.6f;
                HpDamageBuffRank = 1f;
            }
            if (CardRank == 2)
            {
                DamageRank = 1.8f;
                HpDamageBuffRank = 1.2f;
            }
            if (CardRank == 3)
            {
                DamageRank = 2.0f;
                HpDamageBuffRank = 1.5f;
            }


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

            playerHp = player.GetComponent<CharacterStatus>().Hp;

            DisHp = playerHp * 0.2f;

            finalDamage1 = playerAd * DamageRank * enemyDf * criD * ED - enemyDd;


            if(playerHp - DisHp >= 1)
            {
                player.GetComponent<CharacterStatus>().Hp -= DisHp;
            }

            finalDamage2 = DisHp * HpDamageBuffRank * enemyDf * criD * ED - enemyDd;


            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage1;
            Invoke("AdditionalAttck", 0.2f);
            Debug.Log(DisHp.ToString("F0") + "의 체력을 잃고, " + finalDamage2.ToString("F0") + "의 추가 피해와 " + finalDamage1.ToString("F0") + "의 물리피해를 입힙니다.");
            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject, 0.3f);
            enabled = false;
        }
    }

    private void AdditionalAttck()
    {
        enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage2;
    }
}
