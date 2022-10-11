using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyGolemHitBox : Battle_System
{
    public Animator myAnim;
    public TMPro.TextMeshProUGUI dmgText;
    public GameObject mymonster;

    private void OnTriggerEnter(Collider other)
    {    
        if (other.gameObject.layer == LayerMask.NameToLayer("Player_Attack"))
        {
            myAnim.Play("GetHit");
            float dmg = Damaged(Player.atk, mymonster.GetComponent<PolyGolem>().myStat.DEF);
            dmgText.text = dmg.ToString();
            dmgText.gameObject.SetActive(true);
            mymonster.GetComponent<PolyGolem>().myStat.HP_cur -= dmg;
        }
    }
}
