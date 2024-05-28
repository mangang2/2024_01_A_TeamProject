using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour
{
    [SerializeField] Text startPauseText;
    bool pauseActive = false;

    // Start is called before the first frame update
    public void pauseBth()
    {
        if (pauseActive)
        {
            Time.timeScale = 1;
            pauseActive = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseActive = true;
        }

        startPauseText.text = pauseActive ? "풀기" : "일시정지";
    }

}
