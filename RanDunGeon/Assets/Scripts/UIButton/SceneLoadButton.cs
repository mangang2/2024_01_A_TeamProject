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
                LoadSceneController.LoadScene(SceneName);
            }
            else
            {
                SceneManager.LoadScene(SceneName);
            }
        }
    }
}
