using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStand : MonoBehaviour
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
        float playerMaxHp;
        float hpTemp = 0;
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
            playerMaxHp = player.GetComponent<CharacterStatus>().MaxHp;
            playerHp = player.GetComponent<CharacterStatus>().Hp;


            if (CardRank == 1)
            {
                DamageRank = 0.5f;
                HpDamageBuffRank = 0.3f;
            }
            if (CardRank == 2)
            {
                DamageRank = 0.5f;
                HpDamageBuffRank = 0.4f;
            }
            if (CardRank == 3)
            {
                DamageRank = 0.5f;
                HpDamageBuffRank = 0.6f;
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



            hpTemp = playerMaxHp - playerHp;

            finalDamage1 = playerAd * DamageRank * enemyDf * criD * ED - enemyDd;

            finalDamage2 = hpTemp * HpDamageBuffRank * enemyDf * criD * ED - enemyDd;

            player.GetComponent<CharacterStatus>().Recover = hpTemp * 0.7f;

            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage1;
            Invoke("AdditionalAttck", 0.2f);
            Debug.Log((hpTemp * 0.7f).ToString("F0") + "의 체력을 회복하고, " + finalDamage2.ToString("F0") + "의 추가 피해와 " + finalDamage1.ToString("F0") + "의 물리피해를 입힙니다.");
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

