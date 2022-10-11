using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Damaged_Player : Battle_System
{
    public Animator playerAnim;
    public TextMeshProUGUI playerstate;

    public Image Hp_Bar_Front;
    public Image Hp_Bar_Back;

    public GameObject slime;
    public GameObject turtle;
    public GameObject polygolem;
    public TMPro.TMP_Text showHP;

    public GameObject Hitbox;

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        showHP.text = Player.hp_cur.ToString() + " / " + Player.hp.ToString();
        Player_HP_SyncBar();
        if (Player.hp_cur <= 0)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            playerAnim.Play("WGS_Die");
        }
    }
    void Player_HP_SyncBar()
    {
        Hp_Bar_Front.fillAmount = Player.hp_cur / Player.hp;

        if (Hp_Bar_Back.fillAmount > Hp_Bar_Front.fillAmount)
        {
            Hp_Bar_Back.fillAmount = Mathf.Lerp(Hp_Bar_Back.fillAmount, Hp_Bar_Front.fillAmount, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        #region ������ ����
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy_atk")) // �������� �¾������
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player")) // �÷��̾ �¾����� 
            {
                int dmg = Damaged(Golem.golemAtk, Player.def);
                Player.hp_cur -= dmg;
                playerAnim.Play("WGS_Damaged_Front");
                playerstate.color = new Color(1, 0, 0, 1);
                playerstate.text = dmg.ToString();
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Defens")) //��������
            {
                playerstate.color = new Color(0, 0, 1,1);
                playerstate.text = "���";
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Parring")) //�и�������
            {
                playerAnim.Play("WGS_Counter_Attack_Root");
                Player.attakdmg = Damaged(Player.atk, Golem.golemDef, 1.5f, 1.8f);
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "ī����";
                playerstate.gameObject.SetActive(true);
            }
        }
        #endregion

        #region ������� ����
        //������
        if (other.gameObject.layer == LayerMask.NameToLayer("Slime_Attack"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player")) // �÷��̾ �¾����� 
            {
                int dmg = Damaged(slime.GetComponent<Slime>().myStat.ATK, Player.def);
                Player.hp_cur -= dmg;
                playerAnim.Play("WGS_Damaged_Front");
                playerstate.color = new Color(1, 0, 0, 1);
                playerstate.text = dmg.ToString();
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Defens")) //��������
            {
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "���";
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Parring")) //�и�������
            {
                playerAnim.Play("WGS_Counter_Attack_Root");
                Player.attakdmg = Damaged(Player.atk, Golem.golemDef, 1.5f, 1.8f);
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "ī����";
                playerstate.gameObject.SetActive(true);
            }

        }

        //��Ʋ
        if (other.gameObject.layer == LayerMask.NameToLayer("Turtle_Attack"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player")) // �÷��̾ �¾����� 
            {
                int dmg = Damaged(turtle.GetComponent<Turtle>().myStat.ATK, Player.def);
                Player.hp_cur -= dmg;
                playerAnim.Play("WGS_Damaged_Front");
                playerstate.color = new Color(1, 0, 0, 1);
                playerstate.text = dmg.ToString();
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Defens")) //��������
            {
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "���";
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Parring")) //�и�������
            {
                playerAnim.Play("WGS_Counter_Attack_Root");
                Player.attakdmg = Damaged(Player.atk, Golem.golemDef, 1.5f, 1.8f);
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "ī����";
                playerstate.gameObject.SetActive(true);
            }
        }

        //�̴ϰ�
        if (other.gameObject.layer == LayerMask.NameToLayer("PolyGolem_Attack"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Player")) // �÷��̾ �¾����� 
            {
                int dmg = Damaged(polygolem.GetComponent<PolyGolem>().myStat.ATK, Player.def);
                Player.hp_cur -= dmg;
                playerAnim.Play("WGS_Damaged_Front");
                playerstate.color = new Color(1, 0, 0, 1);
                playerstate.text = dmg.ToString();
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Defens")) //��������
            {
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "���";
                playerstate.gameObject.SetActive(true);
            }

            if (gameObject.layer == LayerMask.NameToLayer("Parring")) //�и�������
            {
                playerAnim.Play("WGS_Counter_Attack_Root");
                Player.attakdmg = Damaged(Player.atk, Golem.golemDef, 1.5f, 1.8f);
                playerstate.color = new Color(0, 0, 1, 1);
                playerstate.text = "ī����";
                playerstate.gameObject.SetActive(true);
            }
        }
        #endregion
    }

}
