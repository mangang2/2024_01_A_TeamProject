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
                return "ī�带 Ȧ���ϸ� �� ȿ���� Ȯ�� �� �� �ֽ��ϴ�.";
            case 1:
                return "�������� ��ų���� ������ �����մϴ�. (������ ���� ��ų�� ȿ����������..?)";
            case 2:
                if (GameManager.Instance.CharLevel[0] < 11)
                    return "11���� �޼��� Ư���� ����� �رݵ�����...";
                else
                    return "�������ͽ� ��ȭ�� %��ġ ��ȭ�� +��ġ ��ȭ �� ������ �ֽ��ϴ�.";
            case 3:
                return "�������� �پ��� ī�带 �Ǹ��մϴ�! �������� ��㵵 �Բ� ������!";
            case 4:
                return "10�� �ȿ� Ŭ���� �Ѵٸ� ��� ������ �þ�ϴ�!";
            case 5:
                return "�������� ���� �� ���� Ư���� ī����� �������� ������ ���� ȹ�� �����մϴ�.";
            case 6:
                return "�������� ��͵��� ���� 4�ܰ�� �����ϴ�.";
            case 7:
                return "�ܰ迡 ���� ��ų�� ȿ���� �ٲ�� ī�嵵 �ִٴ� ���!";
            case 8:
                return "������ ī��� �ִ� 5�� ������ ������ �� �ֽ��ϴ�.";
            case 9:
                return "1-3 / 1-5 / 2-5 �������� Ŭ����� �ִ� ���� ������ ����մϴ�.";
            default:
                return "���� �� �ؽ�Ʈ�� ���δٸ� ���� ������ �߻��� ������..��";
        }
    }

}
