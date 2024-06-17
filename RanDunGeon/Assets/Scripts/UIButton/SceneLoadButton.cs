using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadButton : MonoBehaviour
{
    [SerializeField]
    private GameObject CheckUI;

    [SerializeField]
    private bool needChecking = false;

    [SerializeField]
    private bool needLoading = true;

    [SerializeField]
    private string SceneName;

    [SerializeField]
    private string InfoText;

    public void ClickOn()
    {
        //GameManager.Instance.SaveData();
        if(needChecking)
        {
            CheckUI.GetComponent<CheckUI>().SendInfo(SceneName, InfoText);
            CheckUI.SetActive(true);
        }
        else
        {
            if(needLoading)
            {
                if (SceneName == "Restart")
                {
                    LoadSceneController.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                LoadSceneController.LoadScene(SceneName);
            }
            else
            {
                if (SceneName == "Restart")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else if (SceneName == "MainScene")
                {
                    if(GameManager.Instance.LastSceneIsMain == true)
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                    else if(GameManager.Instance.LastSceneIsMain == false)
                    {
                        SceneManager.LoadScene("Stage");
                    }
                }
                else SceneManager.LoadScene(SceneName);
            }
        }
    }
}
