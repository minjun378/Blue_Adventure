using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_System : MonoBehaviour
{

    public int Damaged(float atk, float def,float min =0.9f,float max=1.3f)
    {
        int dmg = 0;
        dmg = (int)((atk* (float)System.Math.Truncate(Random.Range(min, max) * 100) / 100) - def);
        return dmg;
    }
}
