using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListTurnOff : MonoBehaviour
{
    public GameObject ItemListGroup;

    private GameManager GM;

    public void Start()
    {
        GM = GameManager.Instance;
    }

    public void ClickOn()
    {
        GM.UsingItemCheck();
        ItemListGroup.SetActive(false);
    }
}
