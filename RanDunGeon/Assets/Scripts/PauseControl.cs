using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;
    private bool pauseActive = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseButton();
        }
    }

    // Start is called before the first frame update
    public void pauseButton()
    {
        if (pauseActive)
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
            pauseActive = false;
        }
        else
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
            pauseActive = true;
        }
    }

}
