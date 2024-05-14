using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCombineDotsDamage : MonoBehaviour
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
        float dotsDamageSum = 0;
        float damage = 0;
        float finalDamage;

        int CardRank;

        float SumDamageRank = 0;
        float DamageRank = 0;

        if (GetComponent<CardState>().skill == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;

            enemyDf = enemy.GetComponent<CharacterStatus>().Defense;
            enemyDd = enemy.GetComponent<CharacterStatus>().DownDamage;

            if (CardRank == 1)
            {
                SumDamageRank = 0.7f;
                DamageRank = 0.2f;
            }
            if (CardRank == 2)
            {
                SumDamageRank = 0.9f;
                DamageRank = 0.3f;
            }
            if (CardRank == 3)
            {
                SumDamageRank = 1.1f;
                DamageRank = 0.4f;
            }

            for (int i = transform.childCount -1 ; i >= 0 ; i--)
            {
                dotsDamageSum += enemy.transform.GetChild(i).GetComponent<DotsDamage>().ReturnDatsDamage() * SumDamageRank;
                enemy.transform.GetChild(i).GetComponent<DotsDamage>().Turn = 0;
            }
            damage = playerAd * DamageRank * enemyDf - enemyDd;
            finalDamage = damage + dotsDamageSum;
            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage;

            Debug.Log(damage.ToString("F0") + "만큼의 마법 피해를 입히고, 적의 지속피해를 합산하여 " + dotsDamageSum.ToString("F0") + "만큼의 피해를 입힙니다.");

            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            Destroy(gameObject);
        }
    }
}
