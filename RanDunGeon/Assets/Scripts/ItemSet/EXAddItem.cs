using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAddItem : MonoBehaviour
{
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Semicolon))
        {
            float value = 3;
            int type = 2;
            ItemStatusClass temp = new ItemStatusClass();
            temp.ItemType = UnityEngine.Random.Range(0, 6);
            switch(temp.ItemType)
            {
                case 0:
                    value = UnityEngine.Random.Range(6f, 10f);
                    type = UnityEngine.Random.Range(1, 3);
                    break;
                case 1:
                    value = UnityEngine.Random.Range(4f, 7f);
                    type = UnityEngine.Random.Range(1, 3);
                    break;
                case 2:
                    value = UnityEngine.Random.Range(4f, 7f);
                    type = UnityEngine.Random.Range(1, 3);
                    break;
                case 3:
                    value = UnityEngine.Random.Range(3f, 5f);
                    type = 2;
                    break;
                case 4:
                    value = UnityEngine.Random.Range(4f, 8f);
                    type = 2;
                    break;
                case 5:
                    value = UnityEngine.Random.Range(1f, 3f);
                    type = 2;
                    break;
            }
            value = Mathf.Floor(value * 10f) / 10f;

            if (type == 1)
                value += 10000;
            else
                value += 20000;

            temp.ItemValue = value;
            GM.ItemList.Add(temp);
            Debug.Log("Add, Now List Count : " + GM.ItemList.Count);
        }
    }
}
