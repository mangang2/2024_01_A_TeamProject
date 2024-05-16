using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsDamage : MonoBehaviour
{    
    public float Damage;
    public int Turn;
    public float enemyDf;
    public float enemyDd;
    public float finalDamage;

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if(Turn == 0)
        {
            Destroy(gameObject);
        }

    }

    public void DoDotsDamage()
    {
        enemyDf = enemy.gameObject.GetComponent<CharacterStatus>().Defense;
        enemyDd = enemy.gameObject.GetComponent<CharacterStatus>().DownDamage;

        finalDamage = Damage * enemyDf - enemyDd;
        enemy.gameObject.GetComponent<CharacterStatus>().FinalDamage = finalDamage;
        Debug.Log(Turn.ToString("지속피해 : " + finalDamage.ToString("F0") + "의 지속피해를 입힙니다."));
        Turn -= 1;
    }

    public float ReturnDatsDamage()
    {
        enemyDf = enemy.gameObject.GetComponent<CharacterStatus>().Defense;
        enemyDd = enemy.gameObject.GetComponent<CharacterStatus>().DownDamage;

        finalDamage = (Damage * enemyDf - enemyDd) * Turn;
        return finalDamage;
    }
}
