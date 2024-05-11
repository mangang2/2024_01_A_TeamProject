using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatus : MonoBehaviour
{
    public GameObject TrunManager;
    public Text HpText;
    public Slider HpBar;
    public GameObject RecoveryAmount;
    public GameObject DamageAmount;

    public float Recover;
    public float FinalDamage;

    public float MaxHp;
    public float DefaultAd;
    public float DefaultDefense;
    public float DefaultCriPercent;
    public float DefaultCriDamage;

    public float Hp;
    public float Ad;
    public float Defense;
    public int DownDamage;        //?????? ????!!
    public float CriPercent;
    public float CriDamage;

  
    public float AdBuff = 1;
    public float DefenseBuff = 1;
    public float CriPercentBuff = 0;
    public float CriDamageBuff = 0;

  
    public float AdDebuff = 1;
    public float DefenseDebuff = 1;
    public float CriPercentDebuff = 0;
    public float CriDamageDebuff = 0;


    public int AdBuffTurn;
    public int AdDeBuffTurn;
    public int DefenseBuffTurn;
    public int DefenseDebuffTurn;
    public int DownDamageTurn;
    public int CriPercentBuffTrun;
    public int CriPercentDebuffTrun;
    public int CriDamageBuffTrun;
    public int CriDamageDebuffTrun;

    private bool TurnDown = true;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;
        HpBar.value = Hp / MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.tag == "Player")
        {
            if (TrunManager.GetComponent<TurnManager>().pTurn == true && TurnDown == true)
            {
                if (AdBuffTurn > 0) AdBuffTurn--;
                if (AdDeBuffTurn > 0) AdDeBuffTurn--;
                if (DefenseBuffTurn > 0) DefenseBuffTurn--;
                if (DefenseDebuffTurn > 0) DefenseDebuffTurn--;
                if (DownDamageTurn > 0) DownDamageTurn--;
                if (CriPercentBuffTrun > 0) CriPercentBuffTrun--;
                if (CriPercentDebuffTrun > 0) CriPercentDebuffTrun--;
                if (CriDamageBuffTrun > 0) CriDamageBuffTrun--;
                if (CriDamageDebuffTrun > 0) CriDamageDebuffTrun--;
                TurnDown = false;
            }

            if (TrunManager.GetComponent<TurnManager>().eTurn == true) TurnDown = true;
        }

        if (gameObject.transform.tag == "Enemy")
        {
            if (TrunManager.GetComponent<TurnManager>().eTurn == true && TurnDown == true)
            {
                if (AdBuffTurn > 0) AdBuffTurn--;
                if (AdDeBuffTurn > 0) AdDeBuffTurn--;
                if (DefenseBuffTurn > 0) DefenseBuffTurn--;
                if (DefenseDebuffTurn > 0) DefenseDebuffTurn--;
                if (DownDamageTurn > 0) DownDamageTurn--;
                if (CriPercentBuffTrun > 0) CriPercentBuffTrun--;
                if (CriPercentDebuffTrun > 0) CriPercentDebuffTrun--;
                if (CriDamageBuffTrun > 0) CriDamageBuffTrun--;
                if (CriDamageDebuffTrun > 0) CriDamageDebuffTrun--;
                TurnDown = false;
            }

            if (TrunManager.GetComponent<TurnManager>().pTurn == true) TurnDown = true;
        }

        if(Hp > MaxHp)
        {
            Hp = MaxHp;
        }

        if(FinalDamage != 0)
        {
            DamageAmount.GetComponent<TextMeshPro>().text = FinalDamage.ToString("F0");
            Instantiate(DamageAmount).transform.position = new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z);
            Hp -= FinalDamage;
            FinalDamage = 0;
        }

        if(Recover != 0)
        {
            RecoveryAmount.GetComponent<TextMeshPro>().text = Recover.ToString("F0");
            Instantiate(RecoveryAmount).transform.position = new Vector3(gameObject.transform.position.x, 6, gameObject.transform.position.z);
            Hp += Recover;
            Recover = 0;
        }

        if (Hp > 0)
        {
            HpText.text = "Hp : " + Hp.ToString("F0");
            HpBar.value = Hp / MaxHp;
        }
        else if (Hp < 0) 
        {
            HpText.text = "Die";
        }

        if (AdBuffTurn == 0) AdBuff = 1;
        if (AdDeBuffTurn == 0) AdDebuff = 1;
        Ad = DefaultAd * AdBuff * AdDebuff;

        if (DefenseBuffTurn == 0) DefenseBuff = 1;
        if (DefenseDebuffTurn == 0) DefenseDebuff = 1;
        Defense = 100 / (100 + DefaultDefense * DefenseBuff * DefenseDebuff);

        if(DownDamageTurn == 0) DownDamage = 0;

        if (CriPercentBuffTrun == 0) CriPercentBuff = 0;
        if (CriPercentDebuffTrun == 0) CriPercentDebuff = 0;
        CriPercent = DefaultCriPercent + CriPercentBuff - CriPercentDebuff;

        if (CriDamageBuffTrun == 0) CriDamageBuff = 0;
        if (CriPercentDebuffTrun == 0) CriDamageDebuff = 0;
        CriDamage = DefaultCriDamage + CriDamageBuff - CriDamageDebuff;
    }
}
