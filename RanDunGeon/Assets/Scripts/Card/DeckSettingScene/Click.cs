using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    public Camera mainCamera;

    private GameObject temp;
    public bool click = true;
    public bool isHold = false;

    private RaycastHit hit;
    public float holdTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) &&  isHold == false)
        {
            holdTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0) && click == true)
        {
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit);
            click = false;

            if (hit.collider != null)
            {
                if (holdTime < 0.2f)
                {
                    if (hit.collider.transform.tag == "Card")
                    {
                        hit.collider.GetComponent<CardState>().CardInfo();
                        holdTime = 0;
                    }
                }
            }
            click = true;

        }

        if (holdTime >= 0.2f)
        {
            
            isHold = true;
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit);
            if (hit.collider.tag == "Card")
            {
                temp = hit.collider.gameObject;
                hit.collider.GetComponent<DeckSceneMove>().CardDrag();
            }
                else
            {
                holdTime = 0;
                temp.GetComponent<DeckSceneMove>().CardDrop();
            }
        }

            if (Input.GetMouseButtonUp(0))
            {
            if (hit.collider.tag == "Card")
            {
                hit.collider.GetComponent<DeckSceneMove>().CardDrop();
            }
            isHold = false;
            holdTime = 0;
            }
    }
}

