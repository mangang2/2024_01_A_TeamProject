using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExDestroyCard : MonoBehaviour
{
    private RaycastHit hit;
    public Camera subCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray cast = subCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawLine(cast.origin, hit.point, Color.red);
            if (Physics.Raycast(cast, out hit))
            {
                GameObject target = hit.collider.gameObject;
                if (target != null)
                {
                    if(target.GetComponent<CardState>().DoMerge == false)
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
