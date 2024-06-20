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

    public bool store, newData = false;

    [SerializeField]
    private GameObject CheckBox;

    [SerializeField]
    private TextMeshProUGUI InfoUI;

    private void Update()
    {
        if(store == false)
        InfoUI.text = InfoText;
    }

    public void SendInfo(string SceneName, string InfoString, bool NewData = false)
    {
        if(NewData == true)
        {
            newData = true;
        }
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
        if (newData == true)
        {
            GameManager.Instance.NewSaveData();
        }

        if (nextScene == "Quit")
        {
            GameManager.Instance.SaveData();
            Application.Quit();
        }
        else if (nextScene == "Restart")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (nextScene == "ItemClear")
        {
            gameObject.SetActive(false);
            GameManager.Instance.ItemClear();
            GameManager.Instance.SaveData();
        }
        else
        {
            LoadSceneController.LoadScene(nextScene);
            GameManager.Instance.Invoke("SaveData",0.1f);
        }
    }

    public void CancelButton()
    {
        CheckBox.SetActive(false);
    }
}
