using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChapterSceneMove : MonoBehaviour
{
    GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameManager.Instance;

        if (GM.nowChapter == 0)
        {
            GM.nowChapter = 1;
        }

        if (GM.nowChapter == 1)
        {
            transform.position = new Vector2(1344, 764);
        }
        else if (GM.nowChapter == 2)
        {
            transform.position = new Vector2(-574, 764);
        }
    }

    private void ChangeScene()
    {

        if (GM.nowChapter == 1)
        {
            transform.DOMoveX(1344, 1);
        }
        else if (GM.nowChapter == 2)
        {
            transform.DOMoveX(-574, 1);
        }
    }

    // Update is called once per frame
    public void Click(int chapter)
    {
        GM.nowChapter = chapter;
        ChangeScene();
    }
}
