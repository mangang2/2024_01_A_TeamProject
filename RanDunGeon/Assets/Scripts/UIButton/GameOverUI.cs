using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshPro text;

    private StageManager stageManager;

    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        text.text = "Win!!";
        reward();
    }

    public void Lose()
    {
        text.text = "Defeat";
    }

    private void reward()
    {
        GM.Gold += stageManager.gold;
    }
}
