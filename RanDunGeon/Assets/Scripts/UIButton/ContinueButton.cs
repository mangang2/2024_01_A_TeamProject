using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ContinueButton : MonoBehaviour
{
    private string path = Path.Combine(Application.dataPath, "TestSaveData.json");

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (File.Exists(path))
        {
            button.enabled = true;
            GetComponent<Image>().color = new Color(255/255f,255/255f,255/255f);
        }
        else
        {
            button.enabled = false;
            GetComponent<Image>().color = new Color(116/255f, 116/255f, 116/255f);
        }    
    }
}
