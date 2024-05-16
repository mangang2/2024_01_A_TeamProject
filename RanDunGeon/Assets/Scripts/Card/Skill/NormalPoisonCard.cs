using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPoisonCard : MonoBehaviour
{
    public GameObject DotDamagePrefabs;
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

        int CardRank;
        int Turn = 3;

        float DamageRank = 1.2f;

        if (GetComponent<CardState>().skill == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1)
            {
                DamageRank = 1.2f;
                Turn = 3;
            }
            if (CardRank == 2)
            {
                DamageRank = 1.7f;
                Turn = 3;
            }
            if (CardRank == 3)
            {
                DamageRank = 2.3f;
                Turn = 2;
            }

            DotDamagePrefabs.GetComponent<DotsDamage>().Damage = playerAd * DamageRank;
            DotDamagePrefabs.GetComponent<DotsDamage>().Turn = Turn;
            GameObject DotDamageObject = Instantiate(DotDamagePrefabs);
            DotDamageObject.transform.parent = enemy.transform;

            Debug.Log(Turn.ToString("F0") + "턴 동안 매턴 " + (playerAd * DamageRank).ToString("F0") + "의 지속피해를 입힙니다.");
            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skill = false;
            Destroy(gameObject);
        }
    }
}
