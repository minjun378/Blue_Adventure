using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Slider myHP;

    public void Initialize(Transform Root, float Height)
    {
        StartCoroutine(Following(Root, Height));
    }
    IEnumerator Following(Transform Root, float Height)
    {
        while (Root != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(Root.position);
            pos.y += Height;
            this.GetComponent<RectTransform>().anchoredPosition = pos;
            yield return null;
        }
    }
}
