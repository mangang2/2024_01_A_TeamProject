using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject TurnManager;
    public bool eTurn = false;

    private GameObject player;
    private GameObject enemy;
    private bool skillWork = false;
    private int EWCount;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (EWCount == 0)
        {
            eTurn = false;
        }

        int skillNum;

        EWCount = TurnManager.GetComponent<TurnManager>().EWorkCount;

        if (skillWork == false && EWCount > 0 && eTurn == true)
        {
            skillNum = Random.Range(1, 4);

            if (skillNum == 1) Invoke("Skill_1", 0.5f);
            if (skillNum == 2) Invoke("Skill_2", 0.5f);
            if (skillNum == 3) Invoke("Skill_3", 0.5f);
            skillWork = true;
        }
    }

    private void Skill_1()
    {
 

        float enemyAd;
        float playerDf;
        float playerDd;
        float finalDamage;

        enemyAd = enemy.GetComponent<CharacterStatus>().Ad;
        playerDf = player.GetComponent<CharacterStatus>().Defense;
        playerDd = player.GetComponent<CharacterStatus>().DownDamage;

        finalDamage = enemyAd * 1.5f * playerDf - playerDd;

        player.GetComponent<CharacterStatus>().FinalDamage = finalDamage;

        Debug.Log("적-" + (enemyAd * 2).ToString("F0") + "의 물리피해를 입힙니다.");

        StopSkill();
        //Invoke("StopSkill", 0.5f);
    }

    private void Skill_2()
    {
        float DownHp;
        float UpAd;
        float finalAd;

        if (EWCount == 2)
        {
            enemy.GetComponent<CharacterStatus>().Hp *= 0.95f;
            DownHp = enemy.GetComponent<CharacterStatus>().Hp * 0.05f;
            enemy.GetComponent<CharacterStatus>().AdBuffTurn = 1;
            enemy.GetComponent<CharacterStatus>().AdBuff = 1.10f;
            UpAd = enemy.GetComponent<CharacterStatus>().DefaultAd * 0.1f;
            finalAd = enemy.GetComponent<CharacterStatus>().Ad;



            Debug.Log("적-자신의 체력을 " + DownHp.ToString("F0") + "만큼 소모하고, 1턴동안 공격력을 " + UpAd.ToString("F0") + "만큼 올린다");

            StopSkill();
            //Invoke("StopSkill", 0.5f);
        }
        else if (EWCount == 1)
        {
            skillWork = false;
        }
    }

    private void Skill_3()
    {


        float enemyAd;
        float playerDf;
        float playerDd;
        float finalDamage;

        enemyAd = enemy.GetComponent<CharacterStatus>().Ad;
        playerDf = player.GetComponent<CharacterStatus>().Defense;
        playerDd = player.GetComponent<CharacterStatus>().DownDamage;

        finalDamage = enemyAd * 1f * playerDf - playerDd;

        player.GetComponent<CharacterStatus>().FinalDamage = finalDamage;

        enemy.GetComponent<CharacterStatus>().Recover = finalDamage * 0.15f;

        Debug.Log("적-" + (enemyAd * 1.5f).ToString("F0") + "의 물리피해를 입히고, " + (finalDamage * 0.15f).ToString("F0") + "만큼 체력을 흡수합니다.");

        StopSkill();
        //Invoke("StopSkill", 0.5f);
    }

    private void StopSkill()
    {
        TurnManager.GetComponent<TurnManager>().EWorkCount--;
        skillWork = false;
    }
}
