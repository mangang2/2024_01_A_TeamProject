using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyAI : MonoBehaviour
{
    public TurnManager TurnManager;
    public StageManager StageManager;
    public bool eTurn = false;
    public int MonsterNum;

    private GameObject player;
    private GameObject enemy;
    private bool skillWork = false;
    private int EWCount;
    private int skillNum;

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
            StopAllCoroutines();
        }

        EWCount = TurnManager.EWorkCount;

        if (skillWork == false && EWCount > 0 && eTurn == true)
        {
            skillWork = true;
            StartCoroutine(UseSkill());
        }
    }

    private IEnumerator UseSkill()
    {
        float timecheck = 0;
        while(timecheck < 0.5f)
        {
            timecheck += Time.deltaTime;
            yield return null;
        }

        MonsterNum = StageManager.MonsterNum;

        skillNum = Random.Range(1, 3);

        switch(MonsterNum)
        {
            case 11:
                if (skillNum == 1) Skill_11_1();
                if (skillNum == 2) Skill_11_2();
                break;

            case 12:
                if (skillNum == 1) Skill_12_1();
                if (skillNum == 2) Skill_12_2();
                break;

        }

    }

    private void StopSkill(int SP = 2)
    {
        TurnManager.EWorkCount -= SP;
        skillWork = false;
    }
}
public partial class EnemyAI
{
    private void Skill_12_1()
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
    }

    private void Skill_12_2()
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
        }
        else if (EWCount == 1)
        {
            skillWork = false;
        }
    }
}//몬스터 12

public partial class EnemyAI
{
    private void Skill_11_1()
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
    }

    private void Skill_11_2()
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
        }
        else if (EWCount == 1)
        {
            skillWork = false;
        }
    }
}//몬스터 11