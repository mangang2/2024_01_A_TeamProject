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
    [SerializeField]
    private int EWCount;
    private int skillNum;

    private float HP;
    [SerializeField]
    private float MaxHp;
    private float AD;
    private float DF;
    private float CriP;
    private float CriD;
    private float ED;
    private float DotED;

    private float playerDF;
    private float playerDD;
    [SerializeField]
    private int nowPhase;
    [SerializeField]
    private float MaxHpSave;

    public GameObject soyeon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        skillWork = false;
        nowPhase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterNum = StageManager.MonsterNum;
        EWCount = TurnManager.EWorkCount;

        if(MonsterNum == 13)
        {
            loadStatus();

            if (nowPhase != 3)
            {
                enemy.GetComponent<CharacterStatus>().invincibility = true;
            }

            if(HP < 0)
            {
                if (nowPhase == 1)
                {
                    enemy.GetComponent<CharacterStatus>().AdPer = 0.1f;
                    nowPhase = 2;
                    enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 2;
                    enemy.GetComponent<CharacterStatus>().Hp = enemy.GetComponent<CharacterStatus>().MaxHp;
                    loadStatus();
                }
                else if (nowPhase == 2)
                {
                    enemy.GetComponent<CharacterStatus>().AdPer = 0.3f;
                    nowPhase = 3;
                    enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 4;
                    enemy.GetComponent<CharacterStatus>().Hp = enemy.GetComponent<CharacterStatus>().MaxHp;
                    loadStatus();
                    enemy.GetComponent<CharacterStatus>().invincibility = false;
                }

            }
        }
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
        if(MonsterNum == 13 && nowPhase == 0)
        {
            nowPhase = 1;
            MaxHpSave = MaxHp;
            enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 1;
            enemy.GetComponent<CharacterStatus>().Hp = MaxHp;
        }
    }

    private float CriCheck()
    {
        if(Random.Range(0f,100f) <= CriP)
        {
            return 1 + CriD * 0.01f;
        }

        return 1;
    }

    private IEnumerator UseSkill()
    {
        Debug.Log("Skill");
        float timecheck = 0;
        while(timecheck < 1)
        {
            timecheck += Time.deltaTime;
            yield return null;
        }



        loadStatus();

        if(EWCount != 0)
        {
            switch (MonsterNum)
            {
                case 11:
                    Skill_11();
                    break;

                case 12:
                    Skill_12();
                    break;

                case 13:
                    Skill_13();
                    break;
            }
        }
        yield return null;
    }

    private void StopSkill(int SP = 2)
    {
        TurnManager.EWorkCount -= SP;
        skillWork = false;
    }
}
public partial class EnemyAI //챕터 1 몬스터
{
    private void Skill_13()
    {
        int temp = Random.Range(1, nowPhase + 1);

        if(temp == 1)
        {
            Debug.Log("늘러붙기");
            player.GetComponent<CharacterStatus>().FinalDamage = AD * (0.7f + HP / MaxHp) * CriCheck() * playerDF - playerDD;

            if (Random.Range(0f, 100f) < 10)
            {
                player.GetComponent<CharacterStatus>().SkipTurn = true;
                Debug.Log("마비 걸기");
            }
            StopSkill();
        }

        if (temp == 2 && player.GetComponent<CharacterStatus>().DefenseDebuffTurn == 0)
        {
            player.GetComponent<CharacterStatus>().DefenseDebuffTurn = 2;
            player.GetComponent<CharacterStatus>().DefenseDebuff = 30;
            Debug.Log("방깎");
            skillWork = false;
        }

        if (temp == 3)
        {
            Debug.Log("치명타 공격");
            player.GetComponent<CharacterStatus>().FinalDamage = AD * 0.7f * (1 + CriD * 0.01f) * playerDF - playerDD;
            StopSkill();
        }
        
    }

    private void Skill_12()
    {
        player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;

        if(Random.Range(0f,100f) < 10)
        {
            player.GetComponent<CharacterStatus>().SkipTurn = true;
        }
        StopSkill();
    }

    private void Skill_11()
    {
        player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;
        StopSkill();
    }
}