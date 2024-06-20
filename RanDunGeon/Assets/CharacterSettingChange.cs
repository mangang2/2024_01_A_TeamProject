using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSettingChange : MonoBehaviour
{
    public GameObject ItemButton;

    void Update()
    {
        if (GameManager.Instance.CharLevel[0] >= 11)
        {
            ItemButton.SetActive(true);
        }
        else
        {
            ItemButton.SetActive(false);
        }
    }
}
