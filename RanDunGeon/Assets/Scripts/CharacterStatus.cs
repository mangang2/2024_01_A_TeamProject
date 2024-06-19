using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterStatus : MonoBehaviour
{
    public GameObject TurnManager;
    public GameObject ClickChecker;
    public Text HpText;
    public Slider HpBar;
    public Slider ShieldBar;
    public GameObject RecoveryAmount;
    public GameObject DamageAmount;
    public GameObject ShieldAmount;
    
    public float Recover;
    public float FinalDamage;

    public int CharNum;

    public int CharLevel;

    [Header("기초 스테이터스")]
    public float DefaultHp;
    public float DefaultAd;
    public float DefaultDefense;
    public float DefaultCriPercent;
    public float DefaultCriDamage;
    public float DefaultEnhanceD;

    [Header("최대 체력")]
    public float MaxHp;

    [Header("스테이터스 % 추가")]
    public float HpPer;
    public float AdPer;
    public float DfPer;

    [Header("스테이터스 정수값 추가")]
    public float HpAdd;
    public float AdAdd;
    public float DfAdd;
    public float CriPercentAdd;
    public float CriDamageAdd;
    public float EnhanceDAdd;

    [Header("스테이터스")]
    public float Hp;
    public float Ad;
    public float Defense;
    public int DownDamage;        //?????? ????!!
    public float CriPercent;
    public float CriDamage;
    public float EnhanceDamage;

    public float LastDefense;

    [Header("버프")]
    public float AdBuff = 0;
    public float DefenseBuff = 0;
    public float CriPercentBuff = 0;
    public float CriDamageBuff = 0;
    public float EnhanceDBuff = 0;

    [Header("디버프")]
    public float AdDebuff = 0;
    public float DefenseDebuff = 0;
    public float CriPercentDebuff = 0;
    public float CriDamageDebuff = 0;
    public float EnhanceDDebuff = 0;

    [Header("버프 턴")]
    public int AdBuffTurn;
    public int DefenseBuffTurn;
    public int DownDamageTurn;
    public int CriPercentBuffTrun;
    public int CriDamageBuffTrun;
    public int EnhanceDBuffTurn;

    [Header("디버프 턴")]
    public int AdDebuffTurn;
    public int DefenseDebuffTurn;
    public int CriPercentDebuffTrun;
    public int CriDamageDebuffTrun;
    public int EnhanceDDebuffTurn;

    [Header("지속피해 데미지 증가")]
    public float EnhanceDotsD;

    [Header("쉴드")]
    public float Shield,MaxShield;
    public int ShieldTurn;

    public bool DotCri;
    public int DotCriTurn;

    public bool SkipTurn, invincibility;

    private bool TurnDown = true;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.transform.tag == "Player")
        {
            LoadStatus();
        }
        DotCri = false;
        SkipTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.tag == "Player")
        {
            if (TurnManager.GetComponent<TurnManager>().pTurn == true && TurnDown == true)
            {
                if (AdBuffTurn > 0) AdBuffTurn--;
                if (AdDebuffTurn > 0) AdDebuffTurn--;
                if (DefenseBuffTurn > 0) DefenseBuffTurn--;
                if (DefenseDebuffTurn > 0) DefenseDebuffTurn--;
                if (DownDamageTurn > 0) DownDamageTurn--;
                if (CriPercentBuffTrun > 0) CriPercentBuffTrun--;
                if (CriPercentDebuffTrun > 0) CriPercentDebuffTrun--;
                if (CriDamageBuffTrun > 0) CriDamageBuffTrun--;
                if (CriDamageDebuffTrun > 0) CriDamageDebuffTrun--;
                if (EnhanceDBuffTurn > 0) EnhanceDBuffTurn--;
                if (EnhanceDDebuffTurn > 0) EnhanceDDebuffTurn--;
                if (ShieldTurn > 0) ShieldTurn--;
                if (DotCriTurn > 0) DotCriTurn--;
                TurnDown = false;
                StartCoroutine(checkDotsDamage());
            }

            if (TurnManager.GetComponent<TurnManager>().eTurn == true) TurnDown = true;
        }

        if (gameObject.transform.tag == "Enemy")
        {
            if (TurnManager.GetComponent<TurnManager>().eTurn == true && TurnDown == true)
            {
                if (AdBuffTurn > 0) AdBuffTurn--;
                if (AdDebuffTurn > 0) AdDebuffTurn--;
                if (DefenseBuffTurn > 0) DefenseBuffTurn--;
                if (DefenseDebuffTurn > 0) DefenseDebuffTurn--;
                if (DownDamageTurn > 0) DownDamageTurn--;
                if (CriPercentBuffTrun > 0) CriPercentBuffTrun--;
                if (CriPercentDebuffTrun > 0) CriPercentDebuffTrun--;
                if (CriDamageBuffTrun > 0) CriDamageBuffTrun--;
                if (CriDamageDebuffTrun > 0) CriDamageDebuffTrun--;
                if (EnhanceDBuffTurn > 0) EnhanceDBuffTurn--;
                if (EnhanceDDebuffTurn > 0) EnhanceDDebuffTurn--;
                if (ShieldTurn > 0) ShieldTurn--;
                if (DotCriTurn > 0) DotCriTurn--;
                TurnDown = false;
                StartCoroutine(checkDotsDamage());
            }

            if (TurnManager.GetComponent<TurnManager>().pTurn == true) TurnDown = true;
        }

        

        if (SkipTurn == true && TurnManager.GetComponent<TurnManager>().pTurn == true)
        {
            Debug.Log("마비");
            GetComponent<SpriteRenderer>().color = Color.yellow;
            Invoke("returnColor", 0.5f);
            TurnManager.GetComponent<TurnManager>().PWorkCount = 0;
            SkipTurn = false;
        }

        if(Hp > MaxHp)
        {
            Hp = MaxHp;
        }

        if (FinalDamage != 0 && Hp > 0)
            checkDamage();

        if(Recover != 0)
        {
            RecoveryAmount.GetComponent<TextMeshPro>().text = Recover.ToString("F0");
            Instantiate(RecoveryAmount).transform.position = new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z);
            if (Hp + Recover <= MaxHp)
                Hp += Recover;
            else
                Hp = MaxHp;
            Recover = 0;
        }

        if (Hp > 0)
        {
            HpText.text = "Hp : " + Hp.ToString("F0");
            HpBar.value = Hp / MaxHp;
        }
        else if (Hp <= 0 && invincibility == false)
        {
            HpText.text = "Die";
        }

        if(Shield > 0)
        {
            ShieldBar.gameObject.SetActive(true);
            ShieldBar.value = Shield / MaxShield;
        }
        else
        {
            ShieldBar.gameObject.SetActive(false);
        }

        if (AdBuffTurn == 0) AdBuff = 0;
        if (AdDebuffTurn == 0) AdDebuff = 0;
        Ad = (DefaultAd * (1 + AdPer) + AdAdd) * (100 + AdBuff - AdDebuff) * 0.01f;

        if (DefenseBuffTurn == 0) DefenseBuff = 0;
        if (DefenseDebuffTurn == 0) DefenseDebuff = 0;
        LastDefense = (DefaultDefense * (1 + DfPer) + DfAdd) * (100 + DefenseBuff - DefenseDebuff) * 0.01f;
        Defense = 500 / (500 + LastDefense);

        if (DownDamageTurn == 0) DownDamage = 0;

        if (CriPercentBuffTrun == 0) CriPercentBuff = 0;
        if (CriPercentDebuffTrun == 0) CriPercentDebuff = 0;
        CriPercent = DefaultCriPercent + CriPercentAdd + CriPercentBuff - CriPercentDebuff;

        if (CriDamageBuffTrun == 0) CriDamageBuff = 0;
        if (CriPercentDebuffTrun == 0) CriDamageDebuff = 0;
        CriDamage = DefaultCriDamage + CriDamageAdd + CriDamageBuff - CriDamageDebuff;

        if (EnhanceDBuffTurn == 0) EnhanceDBuff = 0;
        if (EnhanceDBuffTurn == 0) EnhanceDDebuff = 0;
        EnhanceDamage = 100 + DefaultEnhanceD + EnhanceDAdd + EnhanceDBuff - EnhanceDDebuff;

        if (ShieldTurn == 0) Shield = 0;

        if (DotCriTurn == 0) DotCri = false;
    }

    private IEnumerator checkDotsDamage()
    {
        for(int i = -1; i < transform.childCount; i++)
        {
            if(i >= 0)
            transform.GetChild(i).GetComponent<DotsDamage>().DoDotsDamage();
            if (FinalDamage != 0 && Hp > 0)
                checkDamage();

            yield return new WaitForSeconds(0.5f);
        }

        if (gameObject.tag == "Enemy")
            gameObject.GetComponent<EnemyAI>().eTurn = true;                //자기 턴 활성화

        if(gameObject.tag == "Player")
            ClickChecker.GetComponent<ClickCheck>().ClickAble = true;
            

        yield break;
    }

    private void checkDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 185 / 255, 185 / 255);
        Invoke("returnColor", 0.2f);
        if (Shield > 0)
        {
            float damageTemp = Shield - FinalDamage;
            Debug.Log(damageTemp);
            if (damageTemp >= 0)
            {
                ShieldAmount.GetComponent<TextMeshPro>().text = FinalDamage.ToString("F0");
                Instantiate(ShieldAmount).transform.position = new Vector3(gameObject.transform.position.x, 12, gameObject.transform.position.z);
                Shield -= FinalDamage;
                FinalDamage = 0;
            }
            else if (damageTemp < 0)
            {
                ShieldAmount.GetComponent<TextMeshPro>().text = FinalDamage.ToString("F0");
                Instantiate(ShieldAmount).transform.position = new Vector3(gameObject.transform.position.x, 12, gameObject.transform.position.z);
                DamageAmount.GetComponent<TextMeshPro>().text = (FinalDamage - Shield).ToString("F0");
                Instantiate(DamageAmount).transform.position = new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z);
                Hp -= FinalDamage - Shield;
                FinalDamage = 0;
                Shield = 0;
                ShieldTurn = 0;

            }
        }
        else
        {
            DamageAmount.GetComponent<TextMeshPro>().text = FinalDamage.ToString("F0");
            Instantiate(DamageAmount).transform.position = new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z);
            Hp -= FinalDamage;
            FinalDamage = 0;
        }
    }

    private void returnColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void LoadStatus()
    {
        GameManager temp = GameManager.Instance;
        CharNum = temp.NowChar;

        switch (CharNum)
        {
            case 1:
                CharLevel = temp.CharLevel[0];
                break;
        }
        DefaultHp = temp.DefaultStatus[0];
        DefaultAd = temp.DefaultStatus[1];
        DefaultDefense = temp.DefaultStatus[2];
        DefaultCriPercent = temp.DefaultStatus[3];
        DefaultCriDamage = temp.DefaultStatus[4];

        HpPer = temp.StatusPer[0];
        AdPer = temp.StatusPer[1];
        DfPer = temp.StatusPer[2];

        HpAdd = temp.StatusAdd[0];
        AdAdd = temp.StatusAdd[1];
        DfAdd = temp.StatusAdd[2];
        CriPercentAdd = temp.StatusAdd[3];
        CriDamageAdd = temp.StatusAdd[4];

        MaxHp = DefaultHp * (1 + HpPer * 0.01f) + HpAdd;
        Hp = MaxHp;
        HpBar.value = Hp / MaxHp;
    }

    public void LoadHp()
    {
        MaxHp = DefaultHp * (1 + HpPer * 0.01f) + HpAdd;
        Hp = MaxHp;
        HpBar.value = Hp / MaxHp;
    }

    public void BounsMove()
    {
        Sequence Bouns = DOTween.Sequence();
        Bouns.Append(transform.DOMoveY(transform.position.y + 1, 0.15f));
        Bouns.Append(transform.DOMoveY(transform.position.y, 0.15f));
    }

}
