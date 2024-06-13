using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamageCri : MonoBehaviour
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

        float DamageRank = 0.6f;

        if (GetComponent<CardState>().skillUse == true)
        {
            playerAd = player.GetComponent<CharacterStatus>().Ad;
            CardRank = GetComponent<CardState>().cardRank;

            if (CardRank == 1)
            {
                DamageRank = 0.6f;
                Turn = 3;
            }
            else if (CardRank == 2)
            {
                DamageRank = 0.8f;
                Turn = 3;
            }
            else if (CardRank == 3)
            {
                DamageRank = 1.0f;
                Turn = 3;
                player.GetComponent<CharacterStatus>().DotCriTurn = 3;
                player.GetComponent<CharacterStatus>().DotCri = true;
            }

            DotDamagePrefabs.GetComponent<DotsDamage>().Damage = playerAd * DamageRank;
            DotDamagePrefabs.GetComponent<DotsDamage>().Turn = Turn;
            GameObject DotDamageObject = Instantiate(DotDamagePrefabs);
            DotDamageObject.transform.parent = enemy.transform;
            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject, 0.3f);
            enabled = false;
        }
    }
}
