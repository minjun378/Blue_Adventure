using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : Battle_System
{
    public float Hp;
    public static float Hp_cur;

    public TMPro.TextMeshProUGUI dmgText;
    public TMPro.TMP_Text test;

    public Image Hp_Bar_Front;
    public Image Hp_Bar_Back;

    private void Start()
    {
        Hp_cur = Hp;
    }

    private void Update()
    {
        SyncBar();
        test.text = "GOLEM";
    }

    void SyncBar()
    {
        Hp_Bar_Front.fillAmount = Hp_cur / Hp;

        if (Hp_Bar_Back.fillAmount > Hp_Bar_Front.fillAmount)
        {
            Hp_Bar_Back.fillAmount = Mathf.Lerp(Hp_Bar_Back.fillAmount, Hp_Bar_Front.fillAmount, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player_Attack")) 
        {
            float dmg = Damaged(Player.atk, Golem.golemDef);
            dmgText.text = dmg.ToString();
            dmgText.gameObject.SetActive(true);
            Hp_cur -= dmg;
        }
    }

}
