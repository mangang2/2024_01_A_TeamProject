using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScripts : MonoBehaviour
{
    public Text LevelText;
    public int Level;

    public int NowChar = 1;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
        Invoke("LoadData", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadData()
    {
        switch (NowChar)
        {
            case 1:
                Level = GM.Char_1_Level;
                break;
            default:
                break;
        }
        LevelText.text = "Level : " + Level.ToString();
    }

    public void LevelUp()
    {
        if (Level < 40)
        {
            switch (NowChar)
            {
                case 1:
                    GM.Char_1_Level = ++Level;
                    break;
                default:
                    break;
            }
            LevelText.text = "Level : " + Level.ToString();
            GM.LoadAddStatus();
        }
        else
        {
            Debug.Log("더이상 레벨업 할 수 없어!");
        }
    }
}
