using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int PWorkCount = 0;
    public int EWorkCount = 0;

    public bool pTurn;
    public bool eTurn;
    // Start is called before the first frame update
    void Start()
    {
        PWorkCount = 2;
        pTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PWorkCount == 0 && pTurn == true)
        {
            EWorkCount = 2;
            eTurn = true;
            pTurn = false;
        }

        if(EWorkCount == 0 && eTurn == true)
        {
            PWorkCount = 2;
            pTurn = true;
            eTurn = false;
        }
    }
}
