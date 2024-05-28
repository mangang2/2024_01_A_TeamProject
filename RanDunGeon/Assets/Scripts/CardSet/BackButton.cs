using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    private GameObject CardSelectCavas;

    private void Start()
    {
        CardSelectCavas = GameObject.Find("CardSelect");
    }

    public void OnClick()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {

        while (CardSelectCavas.GetComponent<CanvasGroup>().alpha > 0)
        {
            CardSelectCavas.GetComponent<CanvasGroup>().alpha -= 3 * Time.deltaTime;
            yield return null;
        }

        CardSelectCavas.SetActive(false);
        yield break;
    }
}
