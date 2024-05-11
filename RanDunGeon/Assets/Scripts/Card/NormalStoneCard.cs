using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStoneCard : MonoBehaviour
{
    private GameObject player;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        float playerAd;
        float enemyDf;
        float enemyDd;
        float finalDamage;

        int CardRank;

        float DamageRank = 1.25f;

        if (GetComponent<CardState>().skill == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1) DamageRank = 1.6f;
            if (CardRank == 2) DamageRank = 1.8f;
            if (CardRank == 3) DamageRank = 2.0f;


            enemyDf = enemy.GetComponent<CharacterStatus>().Defense;
            enemyDd = enemy.GetComponent<CharacterStatus>().DownDamage;

            finalDamage = playerAd * DamageRank * enemyDf - enemyDd;

            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage;
            Debug.Log((playerAd * DamageRank).ToString("F0") + "의 물리피해를 입힙니다.");
            GetComponent<CardState>().skill = false;
            Destroy(gameObject);
        }
    }
}
