using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectObject : MonoBehaviour
{
    public float DamageRank;
    public int Turn;

    private int StartTurn, NowTurn;
    private GameObject TurnManager;
    private GameObject player;
    private GameObject enemy;

    private float beforeShield, nowShield;

    private bool nowUsing;
    
    // Start is called before the first frame update
    void Start()
    {
        nowUsing = false;
        TurnManager = GameObject.FindGameObjectWithTag("TurnManager");
        if (transform.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
        else if (transform.tag == "Enemy")
        {
            enemy = GameObject.FindGameObjectWithTag("Player");
            player = GameObject.FindGameObjectWithTag("Enemy");
        }

        if (player.GetComponent<CharacterStatus>().Shield != 0)
        {
            nowShield = player.GetComponent<CharacterStatus>().Shield;
        }
        TurnCheck();
    }


    // Update is called once per frame
    void Update()
    {
        if(TurnManager.GetComponent<TurnManager>().Turn < NowTurn)
        {
            NowTurn--;
            Turn--;
        }

        if(Turn == 0)
        {
            Destroy(this);
        }

        float criP, ED, criD, enemyDf, enemyDd;
        float defence;
        float finalDamage;
        nowShield = player.GetComponent<CharacterStatus>().Shield;
        defence = player.GetComponent<CharacterStatus>().LastDefense;
        enemyDf = enemy.GetComponent<CharacterStatus>().Defense;
        enemyDd = enemy.GetComponent<CharacterStatus>().DownDamage;

        if (nowShield > beforeShield)
        {
            beforeShield = nowShield;
        }
        else if(nowUsing == false && nowShield < beforeShield && enemy.GetComponent<CharacterStatus>(). ShieldTurn > 0)
        {
            nowUsing = true;
            criP = player.GetComponent<CharacterStatus>().CriPercent;
            ED = player.GetComponent<CharacterStatus>().EnhanceDamage * 0.01f;

            if (Random.Range(0f, 100f) <= criP)
            {
                criD = (100 + player.GetComponent<CharacterStatus>().CriDamage) * 0.01f;
                Debug.Log("치명타!");
            }
            else
            {
                criD = 1f;
            }

            finalDamage = defence * DamageRank * enemyDf * criD * ED - enemyDd;
            enemy.GetComponent<CharacterStatus>().FinalDamage = finalDamage;

            beforeShield = nowShield;
            nowUsing = false;
        }
    }

    public void TurnCheck()
    {
        StartTurn = TurnManager.GetComponent<TurnManager>().Turn;
        NowTurn = StartTurn;
    }
}
