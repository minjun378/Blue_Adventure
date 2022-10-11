using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public float dTime;
    public bool is_time =false;

    private void Start()
    {
        StartCoroutine(removeName());
    }

    IEnumerator removeName()
    {
        yield return new WaitForSeconds(dTime);
        Destroy(this.gameObject);
    }
}
