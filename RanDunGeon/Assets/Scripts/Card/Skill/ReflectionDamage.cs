using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionDamage : MonoBehaviour
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
        float CardRank;
        float DamageRank = 1.8f;

        CardRank = GetComponent<CardState>().cardRank;

        if (CardRank == 1)
        {
            DamageRank = 1f;
        }
        if (CardRank == 2)
        {
            DamageRank = 1.8f;
        }
        if (CardRank == 3)
        {
            DamageRank = 2.5f;
        }

        if (GetComponent<CardState>().skillUse == true)
        {
            if(player.GetComponent<ReflectObject>() == null)
            {
                player.AddComponent<ReflectObject>();
            }
            else
            {
                player.GetComponent<ReflectObject>().Turn = 3;
            }

            player.GetComponent<ReflectObject>().DamageRank = DamageRank;

            TurnManager.GetComponent<TurnManager>().PWorkCount--;
            GetComponent<CardState>().skillUse = false;
            Destroy(gameObject, 0.3f);
            enabled = false;
        }
    }

        
}
