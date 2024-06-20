using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Sprite[] ItemSprite = new Sprite[6];
    public Sprite GoldSprite;

    public GameObject rewardLayout, rewardPrefabs;

    public TurnManager turnManager;

    [SerializeField]
    private StageManager stageManager;

    private GameManager GM;

    public void Win()
    {
        text.text = "Win!!";
        reward();
    }

    public void Lose()
    {
        text.text = "Defeat";
    }

    private void reward()
    {
        int rewardDouble = 1;
        GM = GameManager.Instance;
        if (GM.ClearStage < GM.nowChapter * 100 + GM.nowStage)
        {
            GM.ClearStage = GM.nowChapter * 100 + GM.nowStage;
        }
        CardState rewardTemp;

        if (turnManager.Turn > 10)
        {
            rewardDouble = 2;
        }
        else
        {
            rewardDouble = 1;
        }

        for (int i = 0; i < rewardDouble; i++)
        {
            GM.Gold += stageManager.gold;
            GameObject goldTemp = Instantiate(rewardPrefabs);
            goldTemp.transform.SetParent(rewardLayout.transform);
            goldTemp.GetComponentInChildren<TextMeshProUGUI>().text = stageManager.gold.ToString();
            goldTemp.transform.GetChild(1).GetComponent<Image>().sprite = GoldSprite;
        }
        
        if (stageManager.rewardCard != 0 && GM.CardList[stageManager.rewardCard - 1].GetComponent<CardState>().Unlock == false)
        {
            rewardTemp = GM.CardList[stageManager.rewardCard - 1].GetComponent<CardState>();
            rewardTemp.Unlock = true;
            Debug.Log(rewardTemp.gameObject.name + "카드 해금");
            GameObject cardTemp = Instantiate(rewardPrefabs);
            cardTemp.transform.SetParent(rewardLayout.transform);
            cardTemp.transform.GetChild(0).GetComponent<Image>().enabled = true;
            cardTemp.GetComponentInChildren<TextMeshProUGUI>().text = "";
            cardTemp.transform.GetChild(1).GetComponent<Image>().sprite = rewardTemp.gameObject.GetComponent<CardState>().SkillSprite;
        }
            if (stageManager.itemRank != 0)
        {
            for (int i = 0; i < 6; i++)
            {
                ItemStatusClass newItem = new ItemStatusClass();
                ItemStatus newItemStatus = stageManager.GetItem();
                newItem.ItemType = newItemStatus.ItemType;
                newItem.ItemValue = newItemStatus.ItemValue;
                newItem.Used = false;

                GM.ItemList.Add(newItem);

                GameObject itemTemp = Instantiate(rewardPrefabs);
                itemTemp.transform.SetParent(rewardLayout.transform);
                
                itemTemp.transform.GetChild(1).GetComponent<Image>().sprite = ItemSprite[newItemStatus.ItemType];
                itemTemp.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(150, 129.75f);

                string temp = "";

                if (newItemStatus.EnhanceType)
                    temp = newItemStatus.ItemAdd.ToString() + "%";
                else
                    temp = "+" + newItemStatus.ItemAdd.ToString();

                itemTemp.GetComponentInChildren<TextMeshProUGUI>().text = temp;

            }
        }
    }
}
