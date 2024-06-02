using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        Time.timeScale = 1;
        if (nextScene == "Quit")
        {
            Application.Quit();
        }
        else if(nextScene == "Restart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (nextScene == "ItemClear")
        {
            gameObject.SetActive(false);
            GameManager.Instance.ItemClear();
        }
        else
        LoadSceneController.LoadScene(nextScene);
    }

    public void CancelButton()
    {
        CheckBox.SetActive(false);
    }
}
