using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Col_Atk : Battle_System
{
    public Player attack;
    public string Type_atk;

    int comboStep;
    public string dmg;

    public HitStop hitStop;

    private void OnEnable()
    {
        comboStep = attack.comboStep;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            hitStop.StopTime();
        }
    }
}
