using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotsDamage : MonoBehaviour
{
    public GameObject TurnManager;
    public float Damage;
    public int Turn;

    private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        TurnManager = GameObject.FindGameObjectWithTag("TurnManager");
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
        float enemyDf;
        float enemyDd;
        float finalDamage;

        enemyDf = enemy.gameObject.GetComponent<CharacterStatus>().Defense;
        enemyDd = enemy.gameObject.GetComponent<CharacterStatus>().DownDamage;

        finalDamage = Damage * enemyDf - enemyDd;
        enemy.gameObject.GetComponent<CharacterStatus>().FinalDamage = finalDamage;
        Debug.Log(Turn.ToString("지속피해 : " + Damage.ToString("F0") + "의 지속피해를 입힙니다."));
        Turn -= 1;

    }
}
