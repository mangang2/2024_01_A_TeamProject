using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public TextMeshProUGUI StatusInfo;

    public MonsterStatus stageData;

    public Sprite ItemSprite;

    public GameObject RewardPrefab, RewardLayout, Button;

    private GameManager GM;

    private MonsterStatus.Param stageInfo;

    private int chapter, stage;

    private int monsterLV, itemRank;

    public void OnUI(bool open)
    {
        if(open == true)
        {
            Button.GetComponent<Button>().interactable = true;
        }
        else
        {
            Button.GetComponent<Button>().interactable = false;
        }

        for (int i = RewardLayout.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(RewardLayout.transform.GetChild(i).gameObject);
        }

        string infoText;

        GM = GameManager.Instance;
        chapter = GM.nowChapter;
        stage = GM.nowStage;

        stageInfo = stageData.sheets[chapter].list[stage - 1];

        monsterLV = stageInfo.LV;

        itemRank = stageInfo.ItemRank;

        infoText = "스테이지 : " + chapter + "-" + stage + "\n" + "\n" + "권장레벨 : " + monsterLV + "\n" + "\n";

        if(chapter == 1 && stage == 3)
        {
            infoText += "주의!!!" + "\n" + "챕터 1 중간보스 설명입니다.";
        }
         else if (chapter == 1 && stage == 5)
        {
            infoText += "주의!!!" + "\n" + "챕터 1 최종보스 설명입니다.";
        }
        else if (chapter == 2 && stage == 5)
        {
            infoText += "주의!!!" + "\n" + "챕터 2 중간보스 설명입니다.";
        }
        else if (chapter == 2 && stage == 10)
        {
            infoText += "주의!!!" + "\n" + "챕터 3 최종보스 설명입니다.";
        }

        StatusInfo.text = infoText;

        GameObject goldTemp = Instantiate(RewardPrefab);
        goldTemp.transform.SetParent(RewardLayout.transform);
        goldTemp.GetComponentInChildren<TextMeshProUGUI>().text = stageInfo.Gold.ToString();

        if(stageInfo.ItemRank != 0)
        {
            GameObject itemTemp = Instantiate(RewardPrefab);
            itemTemp.transform.SetParent(RewardLayout.transform);

            itemTemp.transform.GetChild(0).GetComponent<Image>().sprite = ItemSprite;
            itemTemp.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(150, 129.75f);
            itemTemp.GetComponentInChildren<TextMeshProUGUI>().text = "레어도 " + itemRank.ToString();
        }

        if (stageInfo.RewardCard != 0)
        {
            GameObject rewardTemp = GM.CardList[stageInfo.RewardCard - 1];
            GameObject cardTemp = Instantiate(RewardPrefab);
            cardTemp.transform.SetParent(RewardLayout.transform);
            cardTemp.GetComponentInChildren<TextMeshProUGUI>().text = rewardTemp.gameObject.GetComponent<CardInfo>().NameText;
        }
    }

    public void YesButton()
    {
        LoadSceneController.LoadScene("GameScene");
    }

    public void NoButton()
    {
        gameObject.SetActive(false);
    }
}
