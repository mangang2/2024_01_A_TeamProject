using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckUI : MonoBehaviour
{
    public string nextScene;

    public string InfoText = "NoneText";

    [SerializeField]
    private GameObject CheckBox;

    [SerializeField]
    private TextMeshProUGUI InfoUI;

    private void Update()
    {
        InfoUI.text = InfoText;
    }

    public void SendInfo(string SceneName, string InfoString)
    {
        if (SceneName != null)
            nextScene = SceneName;
        else
            nextScene = "StartScene";

        if (InfoString != null)
            InfoText = InfoString;
        else
            InfoText = "NoneText";
    }

    public void YesButton()
    {
        if(nextScene == "Quit")
        {
            Application.Quit();
        }
        else
        LoadSceneController.LoadScene(nextScene);
    }

    public void CancelButton()
    {
        CheckBox.SetActive(false);
    }
}
