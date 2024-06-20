using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class EnemyAI : MonoBehaviour
{
    public GameObject DotsDamagePrefab;
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

    private int criStack,bossStack;

    public GameObject soyeon;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        skillWork = false;
        nowPhase = 0;
        criStack = 0;
        bossStack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MonsterNum = StageManager.MonsterNum;
        EWCount = TurnManager.EWorkCount;

        if(criStack > 10)
        {
            criStack = 10;
        }

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
                    enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 4;
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

        if (MonsterNum == 24)
        {
            loadStatus();

            if (nowPhase != 3)
            {
                enemy.GetComponent<CharacterStatus>().invincibility = true;
            }

            if (HP < 0)
            {
                if (nowPhase == 1)
                {
                    Debug.Log("반사! / 쉴드를 파괴 할시 페이즈 전환");
                    nowPhase = 2;
                    enemy.GetComponent<CharacterStatus>().MaxHp = 1;
                    enemy.GetComponent<CharacterStatus>().ShieldTurn = 20;
                    enemy.GetComponent<CharacterStatus>().MaxShield = MaxHpSave / 4;
                    enemy.GetComponent<CharacterStatus>().Shield = MaxHpSave / 4;
                    enemy.GetComponent<CharacterStatus>().DefenseBuffTurn = 20;
                    enemy.GetComponent<CharacterStatus>().DefenseBuff = 50;
                    enemy.AddComponent<ReflectObject>();
                    enemy.GetComponent<ReflectObject>().Turn = 20;
                    enemy.GetComponent<ReflectObject>().DamageRank = 1f;
                    enemy.GetComponent<CharacterStatus>().Hp = enemy.GetComponent<CharacterStatus>().MaxHp;
                    loadStatus();
                }
                else if (nowPhase == 2)
                {
                    enemy.GetComponent<ReflectObject>().Turn = 0;
                    enemy.GetComponent<CharacterStatus>().DefenseBuffTurn = 0;
                    nowPhase = 3;
                    enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 4;
                    enemy.GetComponent<CharacterStatus>().Hp = enemy.GetComponent<CharacterStatus>().MaxHp;
                    loadStatus();
                    enemy.GetComponent<CharacterStatus>().invincibility = false;
                    TurnManager.GetComponent<TurnManager>().Turn = 20;
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
            enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 2;
            enemy.GetComponent<CharacterStatus>().Hp = MaxHp;
        }

        if (MonsterNum == 24 && nowPhase == 0)
        {
            nowPhase = 1;
            MaxHpSave = MaxHp;
            enemy.GetComponent<CharacterStatus>().MaxHp = MaxHpSave / 2;
            enemy.GetComponent<CharacterStatus>().Hp = MaxHp;
        }
    }

    private float CriCheck()
    {
        if(Random.Range(0f,100f) <= CriP)
        {
            return 1 + CriD * 0.01f + criStack * 0.1f;
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
                case 1:
                    Skill_11();
                    break;
                case 11:
                    Skill_11();
                    break;

                case 12:
                    Skill_12();
                    break;

                case 13:
                    Skill_13();
                    break;

                case 21:
                    Skill_21();
                    break;

                case 22:
                    Skill_22();
                    break;

                case 23:
                    Skill_23();
                    break;

                case 24:
                    Skill_24();
                    break;
            }
        }
        yield break;
    }

    private void StopSkill(int SP = 2)
    {
        transform.GetComponent<CharacterStatus>().BounsMove();
        TurnManager.EWorkCount -= SP;
        skillWork = false;
    }
}
public partial class EnemyAI //챕터 2 몬스터
{

    private void Skill_24()
    {
        int temp = Random.Range(0, 6);

        if (nowPhase == 1)
        {
            if (temp < 3)
            {
                Debug.Log("늘러붙기");
                player.GetComponent<CharacterStatus>().FinalDamage = AD * (1.2f + (MaxHp - HP) / MaxHp) * CriCheck() * playerDF - playerDD;

                if (Random.Range(0f, 100f) < 10)
                {
                    player.GetComponent<CharacterStatus>().SkipTurn = true;
                    player.GetComponent<SpriteRenderer>().color = Color.yellow;
                    Debug.Log("마비 걸기");
                }
                StopSkill();
            }
            else
            {
                Debug.Log("자폭 데미지");
                enemy.GetComponent<CharacterStatus>().FinalDamage = HP * 0.1f;
                player.GetComponent<CharacterStatus>().FinalDamage = (HP * 0.1f) * 0.1f * CriCheck() * playerDF - playerDD;
                StopSkill();
            }
        }
        if (nowPhase == 2)
        {
            Debug.Log("방어력 증가");
            enemy.GetComponent<CharacterStatus>().DefenseBuff += 5;
            StopSkill();
        }

        if (nowPhase == 3)
        {
            if (bossStack < 8)
            {

                int temp2 = Random.Range(0, 8);
                if (temp2 < 4)
                {
                    GameObject dots = Instantiate(DotsDamagePrefab);
                    dots.transform.SetParent(player.transform);
                    dots.GetComponent<DotsDamage>().Damage = AD * 1f;
                    dots.GetComponent<DotsDamage>().Turn = 4;
                    Debug.Log("독가루 날리기");
                    StopSkill();
                }

                if (temp2 == 4)
                {
                    if (player.GetComponent<CharacterStatus>().CriDamageDebuffTrun == 0)
                    {
                        player.GetComponent<CharacterStatus>().CriDamageDebuffTrun = 5;
                        player.GetComponent<CharacterStatus>().CriDamageDebuff = 30;
                        Debug.Log("치명타 피해 감소!");
                        player.GetComponent<CharacterStatus>().FinalDamage = AD * 0.3f * playerDF * CriCheck() - playerDD;
                        StopSkill(1);
                    }
                    else
                        Skill_24();
                }


                if (temp2 == 5)
                {
                    if (player.GetComponent<CharacterStatus>().DefenseDebuffTurn == 0)
                    {
                        player.GetComponent<CharacterStatus>().DefenseDebuffTurn = 5;
                        player.GetComponent<CharacterStatus>().DefenseDebuff = 30;
                        Debug.Log("방어력 감소!");
                        player.GetComponent<CharacterStatus>().FinalDamage = AD * 0.7f * playerDF * CriCheck() - playerDD;
                        StopSkill(1);
                    }
                    else
                        Skill_24();
                }


                if (temp2 == 6)
                {
                    if (player.GetComponent<CharacterStatus>().EnhanceDBuffTurn == 0)
                    {
                        Debug.Log("피해증가 감소!");
                        player.GetComponent<CharacterStatus>().EnhanceDBuffTurn = 5;
                        player.GetComponent<CharacterStatus>().EnhanceDBuff = 10;
                        player.GetComponent<CharacterStatus>().FinalDamage = AD * 0.2f * playerDF * CriCheck() - playerDD;
                        StopSkill(1);
                    }
                    else
                        Skill_24();
                }


                if (temp2 == 7)
                {
                    if (player.GetComponent<CharacterStatus>().AdDebuffTurn == 0)
                    {
                        Debug.Log("공격력 감소!");
                        player.GetComponent<CharacterStatus>().AdDebuffTurn = 5;
                        player.GetComponent<CharacterStatus>().AdDebuff = 20;
                        player.GetComponent<CharacterStatus>().FinalDamage = AD * 0.5f * playerDF * CriCheck() - playerDD;
                        StopSkill(1);
                    }
                    else
                        Skill_24();
                }


                bossStack++;
            }
            else
            {
                bossStack = 0;
                int damageRank = 0;

                if (player.GetComponent<CharacterStatus>().CriDamageDebuffTrun == 0) damageRank++;
                if (player.GetComponent<CharacterStatus>().DefenseDebuffTurn != 0) damageRank++;
                if (player.GetComponent<CharacterStatus>().EnhanceDBuffTurn == 0) damageRank++;
                if (player.GetComponent<CharacterStatus>().AdDebuffTurn == 0) damageRank++;


                player.GetComponent<CharacterStatus>().FinalDamage = AD * (1 + damageRank * 0.5f) * playerDF * CriCheck() - playerDD;
                Debug.Log("디버프 개수에 따라 강해지는 필살기");
                StopSkill();
            }
        }

    }

        private void Skill_23()
    {
        float criD = 1;
        int temp = Random.Range(0, 5);
        Debug.Log(temp);
        if (temp < 3)
        {
            criD = CriCheck();
            if (criD == 1)
            {
                criStack++;
                Debug.Log("치명타 스텍 ++");
            }
            else
            {
                criStack = 0;
                Debug.Log("치명타!");
            }
            player.GetComponent<CharacterStatus>().FinalDamage = AD * 2 * playerDF * criD - playerDD;
            StopSkill();
        }

        if (temp == 3)
        {
            if (GetComponent<CharacterStatus>().CriPercentBuffTrun == 0)
            {
                GetComponent<CharacterStatus>().CriPercentBuffTrun = 3;
                GetComponent<CharacterStatus>().CriPercentBuff = 25;
                Debug.Log("치명타율 증가!");
                StopSkill();
            }
            else
                Skill_23();
        }


        if (temp == 4 )
        {
            if (player.GetComponent<CharacterStatus>().DefenseDebuffTurn == 0)
            {
                criD = CriCheck();
                if (criD == 1)
                {
                    criStack++;
                    Debug.Log("치명타 스텍 ++");
                }
                else
                {
                    criStack = 0;
                    Debug.Log("치명타!");
                }
                player.GetComponent<CharacterStatus>().DefenseDebuffTurn = 2;
                player.GetComponent<CharacterStatus>().DefenseDebuff = 30;
                Debug.Log("방어력 감소!");
                player.GetComponent<CharacterStatus>().FinalDamage = AD * 1 * playerDF * criD - playerDD;
                skillWork = false;
                StopSkill();
            }
            else
                Skill_23();
        }

      
    }
    private void Skill_22()
    {
        
        int temp = Random.Range(0, 6);
        Debug.Log(temp);
        if (temp < 3)
        {
            GameObject dots = Instantiate(DotsDamagePrefab);
            dots.transform.SetParent(player.transform);
            dots.GetComponent<DotsDamage>().Damage = AD * 1f;
            dots.GetComponent<DotsDamage>().Turn = 3;
            Debug.Log("독가루 날리기");
            StopSkill();
        }

        if (temp == 3)
        {
            player.GetComponent<CharacterStatus>().FinalDamage = AD * 2 * playerDF * CriCheck() - playerDD;
            Debug.Log("마비 걸기");
            if (Random.Range(0f, 100f) < 10)
            {
                player.GetComponent<CharacterStatus>().SkipTurn = true;
                player.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            StopSkill();
        }

        if (temp == 4)
        {
            if (GetComponent<CharacterStatus>().DotCriTurn == 0)
            {
                Debug.Log("지속피해 치명타!");
                GetComponent<CharacterStatus>().DotCriTurn = 3;
                GetComponent<CharacterStatus>().DotCri = true;
                StopSkill();
            }
            else
                Skill_22();
        }


        if (temp == 5)
        {
            if (player.GetComponent<CharacterStatus>().AdDebuffTurn == 0)
            {
                Debug.Log("공격력 감소!");
                player.GetComponent<CharacterStatus>().AdDebuffTurn = 3;
                player.GetComponent<CharacterStatus>().AdDebuff = 20;
                player.GetComponent<CharacterStatus>().FinalDamage = AD * 1 * playerDF * CriCheck() - playerDD;
                StopSkill();
            }
            else
                Skill_22();
        }

    }

    private void Skill_21()
    {

        int temp = Random.Range(0, 4);
        if (temp < 3)
        {
            GameObject dots = Instantiate(DotsDamagePrefab);
            dots.transform.SetParent(player.transform);
            dots.GetComponent<DotsDamage>().Damage = AD * 1f;
            dots.GetComponent<DotsDamage>().Turn = 3;
            Debug.Log("독가루 날리기");
            StopSkill();
        }

        if (temp == 3)
        {
            player.GetComponent<CharacterStatus>().FinalDamage = AD * 2 * playerDF * CriCheck() - playerDD;

            if (Random.Range(0f, 100f) < 15)
            {
                player.GetComponent<CharacterStatus>().SkipTurn = true;
                player.GetComponent<SpriteRenderer>().color = Color.yellow;
                Debug.Log("마비 걸기");
            }

            StopSkill();
        }
    }
}
public partial class EnemyAI //챕터 1 몬스터
{
    private void Skill_13()
    {
        int temp = Random.Range(0, nowPhase);

        if(temp == 0)
        {
            Debug.Log("늘러붙기");
            player.GetComponent<CharacterStatus>().FinalDamage = AD * (0.7f + HP / MaxHp) * CriCheck() * playerDF - playerDD;

            if (Random.Range(0f, 100f) < 10)
            {
                player.GetComponent<CharacterStatus>().SkipTurn = true;
                player.GetComponent<SpriteRenderer>().color = Color.yellow;
                Debug.Log("마비 걸기");
            }
            StopSkill();
        }

        if (temp == 1)
        {
            if (player.GetComponent<CharacterStatus>().DefenseDebuffTurn == 0)
            {
                player.GetComponent<CharacterStatus>().DefenseDebuffTurn = 2;
                player.GetComponent<CharacterStatus>().DefenseDebuff = 30;
                Debug.Log("방어력 감소!");
                skillWork = false;
            }
            else
                Skill_13();
        }


        if (temp == 2)
        {
            Debug.Log("치명타 공격");
            player.GetComponent<CharacterStatus>().FinalDamage = AD * 0.7f * (1 + CriD * 0.01f) * playerDF - playerDD;
            StopSkill();
        }
        
    }

    private void Skill_12()
    {
        int temp = Random.Range(0, 4);

        if (temp < 3)
        {
            player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;
            if (Random.Range(0f, 100f) < 10)
            {
                player.GetComponent<CharacterStatus>().SkipTurn = true;
                player.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            StopSkill();
        }

        if (temp == 3)
        {
            if (player.GetComponent<CharacterStatus>().DefenseDebuffTurn == 0)
            {
                Debug.Log("방어력 감소!");
                player.GetComponent<CharacterStatus>().DefenseDebuffTurn = 3;
                player.GetComponent<CharacterStatus>().DefenseDebuff = 15;
                player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;
                StopSkill();
            }
            else
                Skill_12();
        }

    }

    private void Skill_11()
    {
        player.GetComponent<CharacterStatus>().FinalDamage = AD * (1f + HP / MaxHp) * CriCheck() * playerDF - playerDD;
        StopSkill();
    }
}