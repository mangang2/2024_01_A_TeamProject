using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Sprite[] ItemSprite = new Sprite[6];

    public GameObject rewardLayout, rewardPrefabs;

    [SerializeField]
    private StageManager stageManager;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        GM = GameManager.Instance;
        if (GM.ClearStage < GM.nowChapter * 100 + GM.nowStage)
        {
            GM.ClearStage = GM.nowChapter * 100 + GM.nowStage;
        }
        CardState rewardTemp;
        GM.Gold += stageManager.gold;

        GameObject goldTemp = Instantiate(rewardPrefabs);
        goldTemp.transform.SetParent(rewardLayout.transform);
        goldTemp.GetComponentInChildren<TextMeshProUGUI>().text = stageManager.gold.ToString();

        if(stageManager.rewardCard != 0)
        {
            rewardTemp = GM.CardList[stageManager.rewardCard - 1].GetComponent<CardState>();
            rewardTemp.Unlock = true;
            Debug.Log(rewardTemp.gameObject.name + "카드 해금");
            GameObject cardTemp = Instantiate(rewardPrefabs);
            cardTemp.transform.SetParent(rewardLayout.transform);
            cardTemp.GetComponentInChildren<TextMeshProUGUI>().text = rewardTemp.gameObject.name;
        }

        if(stageManager.itemRank != 0)
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
                
                itemTemp.transform.GetChild(0).GetComponent<Image>().sprite = ItemSprite[newItemStatus.ItemType];
                itemTemp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(150, 129.75f);

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
