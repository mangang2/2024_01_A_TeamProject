using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButton : MonoBehaviour
{
    public GameObject StageUI;

    public int ClearStage;

    private bool OpenStage;

    public void Update()
    {
        if(ClearStage <= GameManager.Instance.ClearStage)
        {
            OpenStage = true;
        }
        else
        {
            OpenStage = false;
        }
    }

    public void Click(int stage)
    {
        GameManager.Instance.nowStage = stage;
        StageUI.GetComponent<StageButton>().OnUI(OpenStage);
    }
}
