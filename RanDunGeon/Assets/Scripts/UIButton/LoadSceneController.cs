using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    static string nextScene;

    //[SerializeField]
    //Image prograssBar;

    [SerializeField]
    Text LoadingText;

    AsyncOperation op;

    public static void LoadScene(string sceneName, int stageNum = -1)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LodingScene");
            
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        bool temp = false;
        int dotCount = 3;

        while (!op.isDone && dotCount < 4)
        {
            if(dotCount < 3)
            {
                LoadingText.text += ".";
                dotCount++;
            }
            else
            {
                LoadingText.text = "Loading";
                dotCount = 0;
            }
            if(op.progress >= 0.9f && temp == false)
            {
                temp = true;
                Invoke("LoadSceneDone", 3);
                
            }

            yield return new WaitForSeconds(0.5f);               
        }

    }

    private void LoadSceneDone()
    {
        op.allowSceneActivation = true;
    }

}
