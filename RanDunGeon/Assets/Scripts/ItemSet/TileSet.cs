using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSet : MonoBehaviour
{
    [Header("Tile")]
    public GameObject AreaTile;

    [Header("TileGroup")]
    public GameObject[] AreaTileGroup = new GameObject[3];

    private float tempX, tempY;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tileSet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator tileSet()
    {
        int cellNum = 0;

        for (int i = 0; i < 3; i++)
        {

            for (int n = 0; n < i + 3; n++)
            {
                GameObject temp = Instantiate(AreaTile);
                temp.transform.parent = AreaTileGroup[i].transform;
                temp.transform.localPosition = new Vector2(n * 123, 282 + i * 141 - n * 70.5f);
                temp.GetComponent<ItemCellButton>().CellNum = ++cellNum;
                tempX = temp.transform.localPosition.x;
                tempY = temp.transform.localPosition.y;
                yield return null;
            }

            for (int n = 0; n < i + 2; n++)
            {
                GameObject temp = Instantiate(AreaTile);
                temp.transform.parent = AreaTileGroup[i].transform;
                temp.transform.localPosition = new Vector2(tempX, tempY - 141f - n * 141f);
                temp.GetComponent<ItemCellButton>().CellNum = ++cellNum;
                if (n == i + 1)
                {
                    tempX = temp.transform.localPosition.x;
                    tempY = temp.transform.localPosition.y;
                }
                yield return null;
            }

            for (int n = 0; n < i + 2; n++)
            {
                GameObject temp = Instantiate(AreaTile);
                temp.transform.parent = AreaTileGroup[i].transform;
                temp.transform.localPosition = new Vector2(tempX - (n + 1) * 123, tempY - (n + 1) * 70.5f);
                temp.GetComponent<ItemCellButton>().CellNum = ++cellNum;
                if (n == i + 1)
                {
                    tempX = temp.transform.localPosition.x;
                    tempY = temp.transform.localPosition.y;
                }
                yield return null;
            }

            for (int n = 0; n < i + 2; n++)
            {
                GameObject temp = Instantiate(AreaTile);
                temp.transform.parent = AreaTileGroup[i].transform;
                temp.transform.localPosition = new Vector2(tempX - (n + 1) * 123, tempY + (n + 1) * 70.5f);
                temp.GetComponent<ItemCellButton>().CellNum = ++cellNum;
                if (n == i + 1)
                {
                    tempX = temp.transform.localPosition.x;
                    tempY = temp.transform.localPosition.y;
                }
                yield return null;
            }

            for (int n = 0; n < i + 2; n++)
            {
                GameObject temp = Instantiate(AreaTile);
                temp.transform.parent = AreaTileGroup[i].transform;
                temp.transform.localPosition = new Vector2(tempX, tempY + 141f + n * 141f);
                temp.GetComponent<ItemCellButton>().CellNum = ++cellNum;
                if (n == i + 1)
                {
                    tempX = temp.transform.localPosition.x;
                    tempY = temp.transform.localPosition.y;
                }
                yield return null;
            }

            for (int n = 0; n < i + 1; n++)
            {
                GameObject temp = Instantiate(AreaTile);
                temp.transform.parent = AreaTileGroup[i].transform;
                temp.transform.localPosition = new Vector2(tempX + (n + 1) * 123, tempY + (n + 1) * 70.5f);
                temp.GetComponent<ItemCellButton>().CellNum = ++cellNum;
                if (n == i + 1)
                {
                    tempX = temp.transform.localPosition.x;
                    tempY = temp.transform.localPosition.y;
                }
                yield return null;
            }

            yield return null;
        }
    }
}
