using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickCheck : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject TurnManager;
    public GameObject CardManager;
    public GameObject player;
    public GameObject CardInfoUI;


    public bool Click,ClickAble = true;

    private RaycastHit hit;
    [SerializeField]
    private float holdTime = 0;
    private bool holdClick = true;

    private int PWCount;

    // Update is called once per frame
    void Update()
    {
        PWCount = TurnManager.GetComponent<TurnManager>().PWorkCount;

        if(Input.GetMouseButtonDown(0)) 
        { 
            Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit); 
        }

        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime;
        }



        if (Input.GetMouseButtonUp(0) && CardManager.GetComponent<CardManager>().ClickAble == true && Click == true)
        {
            
            Click = false;

            if (hit.collider != null)
            {
                if (holdTime < 0.5f)
                {
                    if (hit.collider.transform.tag == "Card" && ClickAble == true)
                    {
                        if (PWCount > 0)
                        {
                            hit.collider.gameObject.GetComponent<CardState>().skill();
                            player.GetComponent<CharacterStatus>().BounsMove();
                        }
                        
                        holdTime = 0;
                    }
                }
            }
            Click = true;

        }

        if (holdTime >= 0.5f)
        {
            if (holdClick == true)
            {
                if (hit.collider != null)
                {
                    CardInfoUI.SetActive(true);
                    CardInfoUI.GetComponent<CardInfoUI>().UIOn(hit.collider.GetComponent<CardState>().SkillSprite, hit.collider.GetComponent<CardInfo>().NameText, hit.collider.GetComponent<CardInfo>().InfoText);
                }
                holdClick = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                holdTime = 0;
                holdClick = true;
            }
        }
    }
}
