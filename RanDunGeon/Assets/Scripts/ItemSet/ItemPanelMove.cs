using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelMove : MonoBehaviour
{
    Vector2 BeforeMousePos,CenterPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            BeforeMousePos = Input.mousePosition;
            CenterPos = (Vector2)transform.localPosition - BeforeMousePos;
        }

        if(Input.GetMouseButton(1))
        {
            transform.localPosition = (Vector2)Input.mousePosition + CenterPos;
        } //195 110

        if(Mathf.Abs(transform.localPosition.x) > 195)
        {
            transform.localPosition = new Vector2(Mathf.Sign(transform.localPosition.x) * 195, transform.localPosition.y);
        }

        if (Mathf.Abs(transform.localPosition.y) > 110)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, Mathf.Sign(transform.localPosition.y) * 110);
        }
    }
}
