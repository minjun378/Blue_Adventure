using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgmcontroll : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bossbgm;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Golem.bossApear)
        {
            audioSource.clip = bossbgm;
        }
        if (BossHp.Hp_cur <= 0)
        {
            audioSource.Stop();
        }
    }
}
