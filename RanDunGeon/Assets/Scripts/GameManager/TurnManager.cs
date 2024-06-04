using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStatus Player;
    [SerializeField]
    private CharacterStatus Enemy;
    [SerializeField]
    private GameObject OverUI;


    public int PWorkCount = 0;
    public int EWorkCount = 0;

    public bool pTurn;
    public bool eTurn;

    public bool Playing = true;
    // Start is called before the first frame update
    void Start()
    {
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
        }

        if(Player.Hp <= 0 && Playing)
        {
            StopGame();
            OverUI.GetComponent<GameOverUI>().Lose();
        }

        if(Enemy.Hp <= 0 && Playing)
        {
            StopGame();
            OverUI.GetComponent<GameOverUI>().Win();
        }
    }

    private void StopGame()
    {
        Playing = false;
        EWorkCount = 0;
        PWorkCount = 0;
        pTurn = false;
        eTurn = false;
    }
}
