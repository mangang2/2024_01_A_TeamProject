using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusUI : MonoBehaviour
{
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI[] StatusText = new TextMeshProUGUI[6];
    public TextMeshProUGUI DefenceText;

    private GameManager GM;

    private void Start()
    {
        GM = GameManager.Instance;
    }

    void Update()
    {
        LevelText.text = "레벨 : " + GM.CharLevel[0].ToString();

        StatusText[0].text = "체력 : " + (GM.DefaultStatus[0] * (1 + GM.StatusPer[0]) + GM.StatusAdd[0]).ToString("F0");
        StatusText[1].text = "공격력 : " + (GM.DefaultStatus[1] * (1 + GM.StatusPer[1]) + GM.StatusAdd[1]).ToString("F0");
        StatusText[2].text = "방어력 : " + (GM.DefaultStatus[2] * (1 + GM.StatusPer[2]) + GM.StatusAdd[2]).ToString("F0");
        StatusText[3].text = "치명타 확률 : " + (GM.DefaultStatus[3] + GM.StatusAdd[3]).ToString("F0") + "%";
        StatusText[4].text = "치명타 피해 : " + (GM.DefaultStatus[4] + GM.StatusAdd[5]).ToString("F0") + "%";
        StatusText[5].text = "가하는 피해 증가 : " + (GM.DefaultStatus[5] + GM.StatusAdd[5]).ToString("F0") + "%";

        DefenceText.text = "받는 피해가 " + (100*(1-(500 / (500 + (GM.DefaultStatus[2] * (1 + GM.StatusPer[2]) + GM.StatusAdd[2]))))).ToString("F2") + "% 만큼 감소합니다.";
    }
}
