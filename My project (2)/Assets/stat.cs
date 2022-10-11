using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stat : MonoBehaviour
{
    public Turtle myparent;
    public float ATK;
    public float DEF;
    public float HP;
    public float HP_cur;

    private void Awake()
    {
        ATK = myparent.myStat.ATK;
        //DEF = ;
        //HP = ;
        //HP_cur = ;
    }
}
