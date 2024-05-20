using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatalBlow : MonoBehaviour
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

        int CardRank;

        float DamageRank = 1.25f;
        int criDAdd = 70;

        if (GetComponent<CardState>().skill == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;
            criP = player.GetComponent<CharacterStatus>().CriPercent;



            if (CardRank == 1)
            {
                DamageRank = 0.7f;
                criDAdd = 70;
            }
            if (CardRank == 2)
            {
                DamageRank = 1.2f;
                criDAdd = 70;
            }
            if (CardRank == 3)
            {
                DamageRank = 2.7f;
                criDAdd = 100;
            }


            enemyDf = enemy.GetComponent<CharacterStatus>().Defense;
            enemyDd = enemy.GetComponent<CharacterStatus>().DownDamage;

            if (Random.Range(0f, 100f) >= criP)
            {
                criD = (100 + player.GetComponent<CharacterStatus>().CriDamage) * 0.01f + criDAdd * 0.01f;
                Debug.Log("???!");
            }
            else
            {
                criD = 1f;
            }

            finalDamage = playerAd * DamageRank * enemyDf * (criD) - enemyDd;

            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage;
            Debug.Log(finalDamage.ToString("F0") + "? ????? ????.");
            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skill = false;
            Destroy(gameObject, 0.3f);
            enabled = false;
        }
    }
}
