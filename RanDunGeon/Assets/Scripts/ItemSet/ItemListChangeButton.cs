using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemListChangeButton : MonoBehaviour
{
    public int buttonNum;

    public GameObject[] ItemList = new GameObject[6];

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClickOn);
    }

    public void ClickOn()
    {
       for(int i = 0; i < 6; i++)
        {
            ItemList[i].SetActive(false);
        }

        ItemList[buttonNum].SetActive(true);
    }
}
