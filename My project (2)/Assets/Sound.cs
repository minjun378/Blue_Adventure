using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static AudioSource mainbgm;
    public AudioClip main_bgm;
    // Start is called before the first frame update
    private void OnEnable()
    {
        mainbgm = GetComponent<AudioSource>();
        mainbgm.clip = main_bgm;
        mainbgm.Play();
    }
    //private void Awake()
    //{
    //    mainbgm = GetComponent<AudioSource>();
    //    mainbgm.clip = main_bgm;
    //    mainbgm.Play();
    //}
    void Start()
    {
        
    }


}
