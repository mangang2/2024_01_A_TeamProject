using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastSceneCheck : MonoBehaviour
{
    public bool ThisSceneIsMain;

    void Start()
    {
        GameManager.Instance.LastSceneIsMain = ThisSceneIsMain;
    }
}
