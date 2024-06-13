using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject TurnManager;
    public int gold;
    public int itemRank;
    public int rewardCard;
    public int MonsterNum;

[SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject player;
    private CharacterStatus enemyStasus;
    [SerializeField]
    private MonsterStatus monsterStatus;
    [SerializeField]
    private ItemTypePer itemTypePer;
    [SerializeField]
    private ItemRankPer itemRankPer;
    [SerializeField]
    private ItemStatusPer itemStatusPer;

    private float[] TypePer = new float[9];
    private float[] RankPer = new float[4];
    private int chapter;
    private int stage;
    private MonsterStatus.Param monster;

    private void Start()
    {
        chapter = GameManager.Instance.nowChapter;
        stage = GameManager.Instance.nowStage;

        monster = monsterStatus.sheets[chapter].list[stage - 1];

        gold = monster.Gold;
        itemRank = monster.ItemRank;
        rewardCard = monster.RewardCard;
        MonsterNum = monster.MonsterNum;

        if(itemRank > 0)
        LoadItemStatus(itemRank -1);

        enemyStasus = enemy.GetComponent<CharacterStatus>();
        if (enemyStasus)
        {
            LoadEnemyStatus();
        }
    }


    private void LoadEnemyStatus()
    {
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
        TurnManager.GetComponent<TurnManager>().Playing = true;
    }

    private void LoadItemStatus(int rank)
    {
        TypePer[0] = itemTypePer.sheets[0].list[rank].HPPer;
        TypePer[1] = itemTypePer.sheets[0].list[rank].ADPer;
        TypePer[2] = itemTypePer.sheets[0].list[rank].DFPer;
        TypePer[3] = itemTypePer.sheets[0].list[rank].HPAdd;
        TypePer[4] = itemTypePer.sheets[0].list[rank].ADAdd;
        TypePer[5] = itemTypePer.sheets[0].list[rank].DfAdd;
        TypePer[6] = itemTypePer.sheets[0].list[rank].CriPAdd;
        TypePer[7] = itemTypePer.sheets[0].list[rank].CriDAdd;
        TypePer[8] = itemTypePer.sheets[0].list[rank].EDAdd;

        RankPer[0] = itemRankPer.sheets[0].list[rank].ItemRank1;
        RankPer[1] = itemRankPer.sheets[0].list[rank].ItemRank2;
        RankPer[2] = itemRankPer.sheets[0].list[rank].ItemRank3;
        RankPer[3] = itemRankPer.sheets[0].list[rank].ItemRank4;
    }

    private int typeSetting()
    {
        float randomNum = Random.Range(0f, 100f);

        float sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += TypePer[i];
            if (randomNum <= sum && TypePer[i] != 0)
            {
                return i;
            }
        }

        return 0;
    }

    private int rankSetting()
    {
        float randomNum = Random.Range(0f, 100f);

        float sum = 0;

        for (int i = 0; i < 4; i++)
        {
            sum += RankPer[i];
            if (randomNum <= sum && RankPer[i] != 0)
            {
                return i;
            }
        }

        return 0;
    }

    public ItemStatus GetItem()
    {
        ItemStatus newItem = new ItemStatus();
        int typeTemp = typeSetting();
        int rankTemp = rankSetting();

        ItemStatusPer.Param valueList = itemStatusPer.sheets[0].list[typeTemp];

        float valueTemp = 0;

        switch(rankTemp)
        {
            case 0:
                valueTemp = valueList.ItemRank1;
                break;
            case 1:
                valueTemp = valueList.ItemRank2;
                break;
            case 2:
                valueTemp = valueList.ItemRank3;
                break;
            case 3:
                valueTemp = valueList.ItemRank4;
                break;
        }
        Debug.Log(typeTemp);

        switch (typeTemp)
        {
            case 0:
                valueTemp += 20000;
                typeTemp = 0;
                break;
            case 1:
                valueTemp += 20000;
                typeTemp = 1;
                break;
            case 2:
                valueTemp += 20000;
                typeTemp = 2;
                break;
            case 3:
                valueTemp += 10000;
                typeTemp = 0;
                break;
            case 4:
                valueTemp += 10000;
                typeTemp = 1;
                break;
            case 5:
                valueTemp += 10000;
                typeTemp = 2;
                break;
            case 6:
                valueTemp += 20000;
                typeTemp = 3;
                break;
            case 7:
                valueTemp += 20000;
                typeTemp = 4;
                break;
            case 8:
                valueTemp += 20000;
                typeTemp = 5;
                break;
            default:
                break;
        }

        Debug.Log(typeTemp);
        newItem.SetValue(typeTemp, valueTemp, false);

        return newItem;
    }
}
