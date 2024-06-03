using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int gold;
    public int itemRank;

    [SerializeField]
    private GameObject enemy;
    private CharacterStatus enemyStasus;
    [SerializeField]
    private MonsterStatus monsterStatus;

    private void Start()
    {
        enemyStasus = enemy.GetComponent<CharacterStatus>();
        LoadEnemyStatus();
    }


    private void LoadEnemyStatus()
    {
        int chapter = GameManager.Instance.nowChapter;
        int stage = GameManager.Instance.nowStage;
        MonsterStatus.Param monster = monsterStatus.sheets[chapter].list[stage-1];

        gold = monster.Gold;
        itemRank = monster.ItemRank;

        enemyStasus.CharLevel = monster.LV;
        enemyStasus.DefaultHp = monster.HP;
        enemyStasus.DefaultAd = monster.AD;
        enemyStasus.DefaultDefense = monster.Df;
        enemyStasus.DefaultCriPercent = monster.CriP;
        enemyStasus.DefaultCriDamage = monster.CriD;
        enemyStasus.DefaultEnhanceD = monster.ED;
        enemyStasus.EnhanceDotsD = monster.DotsED;

        //Image

        enemyStasus.LoadHp();
    }

    public void GetItem()
    {

    }
}
