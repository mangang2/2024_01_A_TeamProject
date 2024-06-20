using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tips : MonoBehaviour
{
    public TextMeshProUGUI TipsUI;

    // Start is called before the first frame update
    void Start()
    {
        TipsUI.text = "Tips : " + TipsText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            TipsUI.text = "Tips : " + TipsText();
        }
    }

    string TipsText()
    {
        switch (Random.Range(0, 10))
        {
            case 0:
                return "카드를 홀드하면 상세 효과를 확인 할 수 있습니다.";
            case 1:
                return "지속피해 스킬들은 방어력을 무시합니다. (데미지 감소 스킬이 효과적일지도..?)";
            case 2:
                if (GameManager.Instance.CharLevel[0] < 11)
                    return "11레벨 달성시 특별한 기능이 해금될지도...";
                else
                    return "스테이터스 강화는 %수치 강화와 +수치 강화 두 종류가 있습니다.";
            case 3:
                return "상점에선 다양한 카드를 판매합니다! 주인장의 잡담도 함께 말이죠!";
            case 4:
                return "10턴 안에 클리어 한다면 몇몇 보상이 늘어납니다!";
            case 5:
                return "상점에선 구할 수 없는 특별한 카드들은 스테이지 보상을 통해 획득 가능합니다.";
            case 6:
                return "아이템은 희귀도에 따라 4단계로 나뉩니다.";
            case 7:
                return "단계에 따라 스킬의 효과가 바뀌는 카드도 있다는 사실!";
            case 8:
                return "동일한 카드는 최대 5장 까지만 생성될 수 있습니다.";
            case 9:
                return "1-3 / 1-5 / 2-5 스테이지 클리어시 최대 레벨 제한이 상승합니다.";
            default:
                return "지금 이 텍스트가 보인다면 무언가 오류가 발생한 걸지도..ㅠ";
        }
    }

}
