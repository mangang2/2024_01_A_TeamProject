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

    private float HP;
    private float MaxHp;
    private float AD;
    private float DF;
    private float CriP;
    private float CriD;
    private float ED;
    private float DotED;

    private float playerDF;
    private float playerDD;

    public GameObject soyeon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        skillWork = false;
    }

    // Update is called once per frame
    void Update()
    {
        EWCount = TurnManager.EWorkCount;

        if (EWCount == 0)
        {
            eTurn = false;
            StopAllCoroutines();
        }


        if (skillWork == false && EWCount > 0 && eTurn == true)
        {
            skillWork = true;
            StartCoroutine(UseSkill());
        }
    }

    private void loadStatus()
    {
        CharacterStatus temp = enemy.GetComponent<CharacterStatus>();

        HP = temp.Hp;
        MaxHp = temp.MaxHp;
        AD = temp.Ad;
        DF = temp.LastDefense;
        CriP = temp.CriPercent;
        CriD = temp.CriDamage;
        ED = temp.EnhanceDamage;
        DotED = temp.EnhanceDotsD;

        playerDF = player.GetComponent<CharacterStatus>().Defense;
        playerDD = player.GetComponent<CharacterStatus>().DownDamage;
    }

    private float CriCheck()
    {
        if(Random.Range(0f,100f) <= CriP)
        {
            return 1 + CriD;
        }

        return 1;
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

        loadStatus();

        if(EWCount != 0)
        {
            switch (MonsterNum)
            {
                case 11:
                    if (skillNum == 1) Skill_11();
                    break;

                case 12:
                    if (skillNum == 1) Skill_12();
                    break;
            }
        }
    }

    private void StopSkill(int SP = 2)
    {
        TurnManager.EWorkCount -= SP;
        skillWork = false;
    }
}
public partial class EnemyAI //몬스터 12
{
    private void Skill_12()
    {
        player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;

        if(Random.Range(0f,100f) < 10)
        {
            player.GetComponent<CharacterStatus>().SkipTurn = true;
        }


        StopSkill();
    }
}

public partial class EnemyAI //몬스터 11
{
    private void Skill_11()
    {
        player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;
        StopSkill();
    }
}