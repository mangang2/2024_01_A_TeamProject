using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickCheck : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject TurnManager;
    public GameObject CardManager;


    public bool click = true;

    private RaycastHit hit;
    private float holdTime = 0;
    private bool Click = true;

    private int PWCount;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PWCount = TurnManager.GetComponent<TurnManager>().PWorkCount;

        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
        }

        if(Input.GetMouseButtonUp(0) && CardManager.GetComponent<CardManager>().stopDraw == true && click == true)
        {
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit);
            click = false;

            if (hit.collider != null)
            {
                if (holdTime < 0.3f)
                {
                    if (hit.collider.transform.tag == "Card")
                    {
                        if (PWCount > 0)
                        {
                            TurnManager.GetComponent<TurnManager>().PWorkCount--;
                            hit.collider.gameObject.GetComponent<CardState>().skill = true;
                        }
                        
                        holdTime = 0;
                    }
                }
            }
            click = true;

        }

        if (holdTime >= 0.3f)
        {
            if (Click == true)
            {
                Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit);
                if (hit.collider != null)
                {
                    hit.collider.GetComponent<CardState>().CardInfo();
                }
                Click = false;
            }

            if (Input.GetMouseButtonUp(0))
            { 
                holdTime = 0;
                Click = true;
            }
        }
    }
}
