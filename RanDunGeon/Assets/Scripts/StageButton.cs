using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class StageButton : MonoBehaviour
{
    public Image monsterImage;

    public TextMeshProUGUI StatusInfo;

    public MonsterStatus stageData;

    public Sprite ItemSprite,GoldSprite,Mon1,Mon2;

    public GameObject RewardPrefab, RewardLayout, Button;

    private GameManager GM;

    private MonsterStatus.Param stageInfo;

    private int chapter, stage;

    private int monsterLV, itemRank;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            NoButton();
        }
    }

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

        if(chapter == 1)
        {
            monsterImage.GetComponent < Image >().sprite = Mon1;
            monsterImage.GetComponent<RectTransform>().localPosition = new Vector3(-270, 9, 0);
            if (stage < 3)
                infoText += "슬라임" + "\n" + "풀숲에 숨어지내는 초록 슬라임이다.";
            else if (stage < 5)
                infoText += "끈적끈쩍 슬라임" + "\n" + "끈적이는 슬라임 액체는 몸을 마비시키거나 방어자세를 무너뜨리는 효과가 있다.";
            else
                infoText += "주의!!!" + "\n" + "대왕 슬라임" + "\n" + "피해를 입으면 몸집이 줄어들어 약해지지만 빠른 속도로 더욱 강한 공격을 한다.";
        }
        else if (chapter == 2)
        {
            monsterImage.GetComponent<Image>().sprite = Mon2;
            monsterImage.GetComponent<RectTransform>().localPosition = new Vector3(-293, 77, 0);
            if (stage < 5)
                infoText += "먼지 괴물" + "\n" + "탁한 공기들 속에서 태어난 괴물이다." + "\n" + "지속적인 피해와 마비를 입히는 분진을 뿌린다.";
            else if (stage == 5)
                infoText += "고농도 먼지 괴물" + "\n" + "먼지 괴물이 뭉쳐진 모습으로 강화된 지속피해를 사용한다.";
            else if (stage < 10)
                infoText += "사나운 길 고양이" + "\n" + "날카로운 발톱으로 공격하는 길고양이다." + "\n" + "(날카로운 발톱은 계속 손질되어 더욱 날카롭고 위험한 존재가 된다.)";
            else if (stage == 10)
                infoText += "???" + "\n" + "도시 어딘가에서 힘을 길러온 정체 모를 생명체..." + "\n" + "(도시 지역의 최종보스 입니다.)";
        }

        StatusInfo.text = infoText;

        GameObject goldTemp = Instantiate(RewardPrefab);
        goldTemp.transform.SetParent(RewardLayout.transform);
        goldTemp.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
        goldTemp.GetComponentInChildren<TextMeshProUGUI>().text = stageInfo.Gold.ToString() + "G";
        goldTemp.transform.GetChild(1).GetComponent<Image>().sprite = GoldSprite;

        if (stageInfo.ItemRank != 0)
        {
            GameObject itemTemp = Instantiate(RewardPrefab);
            itemTemp.transform.SetParent(RewardLayout.transform);

            itemTemp.transform.GetChild(1).GetComponent<Image>().sprite = ItemSprite;
            itemTemp.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(150, 129.75f);
            itemTemp.GetComponentInChildren<TextMeshProUGUI>().fontSize = 26;
            itemTemp.GetComponentInChildren<TextMeshProUGUI>().text = "Rare" + itemRank.ToString();
        }

        if (stageInfo.RewardCard != 0)
        {
            GameObject rewardTemp = GM.CardList[stageInfo.RewardCard - 1];
            GameObject cardTemp = Instantiate(RewardPrefab);
            cardTemp.transform.SetParent(RewardLayout.transform);
            cardTemp.GetComponentInChildren<TextMeshProUGUI>().enableAutoSizing = true;
            cardTemp.transform.GetChild(0).GetComponent<Image>().enabled = true;
            cardTemp.GetComponentInChildren<TextMeshProUGUI>().text = "";
            cardTemp.transform.GetChild(1).GetComponent<Image>().sprite = rewardTemp.gameObject.GetComponent<CardState>().SkillSprite;
        }
        gameObject.SetActive(true);
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
