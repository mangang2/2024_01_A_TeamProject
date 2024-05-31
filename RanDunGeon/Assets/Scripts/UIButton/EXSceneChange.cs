using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EXSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < 8; i++)
        {
            string num = i.ToString();

            if(Input.inputString == num)
            {
                SceneManager.LoadScene(i);
            }
        }
            
    }
}
