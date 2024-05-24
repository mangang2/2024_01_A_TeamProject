using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCellButton : MonoBehaviour
{
    public ItemStatus itemstatus;

    public int CellRank, CellNum;
    public bool Useable = false;

    private GameManager GM;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (CellRank <= ((GM.CharLevel[GM.NowChar - 1] + 10)/ 10))
        {
            Useable = true;
        }
        else
            Useable = false;

        if (Useable)
            text.text = CellRank.ToString() + " / " + CellNum.ToString();
        else
            text.text = "";
    }

    void OnClick()
    {
        if (Useable)
        {
            Debug.Log("아이템 리스트 화면 실행");
        }
        else
            Debug.Log("해금되지 않은 칸이야!");
    }
}
