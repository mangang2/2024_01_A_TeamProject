using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AmoutTextMove : MonoBehaviour
{
    public TextMeshPro ThisText;
    // Start is called before the first frame update
    void Start()
    {
        ThisText = this.gameObject.GetComponent<TextMeshPro>();
        transform.DOMoveY(transform.position.y + 1, 1f).SetEase(Ease.Linear);
        ThisText.DOFade(0, 1f);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
