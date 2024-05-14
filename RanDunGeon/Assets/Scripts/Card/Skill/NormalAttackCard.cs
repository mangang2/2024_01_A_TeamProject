using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackCard : MonoBehaviour
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
        float criP;
        float criD;

        int CardRank;

        float DamageRank = 1.25f;

        if (GetComponent<CardState>().skill == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;
            criP = player.GetComponent<CharacterStatus>().CriPercent;
 


            if (CardRank == 1) DamageRank = 1.2f;
            if (CardRank == 2) DamageRank = 2.1f;
            if (CardRank == 3) DamageRank = 3.5f;

            
            enemyDf = enemy.GetComponent<CharacterStatus>().Defense;
            enemyDd = enemy.GetComponent<CharacterStatus>().DownDamage;

            if(Random.Range(0f,100f) <= criP)
            {
                criD = (100 + player.GetComponent<CharacterStatus>().CriDamage) * 0.01f;
                Debug.Log("치명타!");
            }
            else
            {
                criD = 1f;
            }

            finalDamage = playerAd * DamageRank * enemyDf * criD - enemyDd;

            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage;
            Debug.Log(finalDamage.ToString("F0") + "의 물리피해를 입힙니다.");
            GetComponent<CardState>().skill = false;
            Destroy(gameObject);
        }
    }
}
