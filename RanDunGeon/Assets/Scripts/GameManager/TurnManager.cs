using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class TurnManager : MonoBehaviour
{
    public GameObject ClickChecker;

    public CharacterStatus Player;
    public CharacterStatus Enemy;
    public GameObject OverUI;

    public TextMeshProUGUI TurnText;

    public int Turn;

    public int PWorkCount = 0;
    public int EWorkCount = 0;

    public bool pTurn;
    public bool eTurn;

    public bool Playing;
    // Start is called before the first frame update
    void Start()
    {
        Playing = false;
        Turn = 20;
        PWorkCount = 2;
        pTurn = true;
           
    }

    // Update is called once per frame
    void Update()
    {

        if (PWorkCount == 0 && pTurn == true && Playing)
        {
            EWorkCount = 2;
            eTurn = true;
            pTurn = false;
        }

        if(EWorkCount == 0 && eTurn == true && Playing)
        {
            PWorkCount = 2;
            pTurn = true;
            eTurn = false;
            ClickChecker.GetComponent<ClickCheck>().ClickAble = false;
            Turn--;
        }

        if (Turn > 0)
        {
            TurnText.text = "���� �� " + Turn + " / 20   �ൿ " + PWorkCount + " / 2";
        }
        else
        {
            TurnText.text = "�� 20 / 20    2 / 2";
            StopGame();
            OverUI.SetActive(true);
            OverUI.GetComponent<GameOverUI>().Lose();
        }

        if (Player.Hp <= 0 && Playing && Player.invincibility == false)
        {
            StopGame();
            OverUI.SetActive(true);
            OverUI.GetComponent<GameOverUI>().Lose();
        }

        if(Enemy.Hp <= 0 && Playing && Enemy.invincibility == false)
        {
            StopGame();
            OverUI.SetActive(true);
            OverUI.GetComponent<GameOverUI>().Win();
        }
    }
    

    private void StopGame()
    {
        ClickChecker.SetActive(false);
        Playing = false;
        EWorkCount = 0;
        PWorkCount = 0;
        pTurn = false;
        eTurn = false;
    }
}
