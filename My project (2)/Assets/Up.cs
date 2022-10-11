using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    [SerializeField] float updist;
    [SerializeField] bool playercheck = false;
    [SerializeField] bool getbutton = false;
    public GameObject text;
    public TMPro.TMP_Text help;
    public GameObject GameClear;
    // Update is called once per frame
    void Update()
    {
        if (playercheck && Input.GetKey(KeyCode.E))
        {
            getbutton = true;
            text.SetActive(false);
        }
        if (getbutton && updist < 6.0f)
        {
            up();
        }
        if (updist >= 6.0f)
        {
            GameClear.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void up()
    {
        updist += Time.deltaTime*2;
        transform.Translate(Vector3.up * Time.deltaTime*2);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playercheck = true;
            text.SetActive(true);
            help.text = "E : ±¸ÇÏ±â";
        }
    }
}
