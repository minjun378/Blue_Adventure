using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public AudioSource golemaudio;
    public AudioClip attacksound;
    public AudioClip walksound;
    public static Animator golemAnim;
    public Transform target;
    #region
    public static float golemSpeed = 4.0f;
    public static float golemAtk = 300.0f;
    public static float golemDef = 50.0f;
    #endregion

    bool enableAct;
    int atkStep;
    public float dist;
    public GameObject Camera;
    public GameObject itemSphere; //������ ����
    public GameObject _BossHp;
    public GameObject _PlayerHp;

    public static bool bossApear = false;

    public GameObject hitbox;
    
    private void Start()
    {
        golemaudio = GetComponent<AudioSource>();
        golemAnim = GetComponent<Animator>();
        enableAct = true;
        
        target = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        dist = (target.position - transform.position).magnitude;
        if (dist < 150.0f && !bossApear)
        {
            golemaudio.Play();
            bossApear = true;
            Camera.SetActive(true);
            golemAnim.Play("Golem_Appear");
        }
        
        if (dist < 150.0f) 
        {
            if (enableAct)
            {
                RotateGolem();
                MoveGolem();
            }
            
            if (BossHp.Hp_cur <= 0)
            {
                golemAnim.Play("Death");
                hitbox.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
              
    }
    void RotateGolem()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
    }

    void MoveGolem()
    {
        if ((target.position - transform.position).magnitude >= 10)
        {
            golemAnim.SetBool("Walk", true);
            transform.Translate(Vector3.forward * golemSpeed * Time.deltaTime,Space.Self);
        }
        if ((target.position - transform.position).magnitude < 10)
        {
            golemAnim.SetBool("Walk", false);
        }
    }

    void GolemAtk()
    {
        if ((target.position - transform.position).magnitude < 10)
        {
            switch (atkStep)
            {
                case 0:
                    atkStep += 1;
                    golemAnim.Play("Golem_AtkA");
                    break;
                case 1:
                    atkStep += 1;
                    golemAnim.Play("Golem_AtkB");
                    break;
                case 2:
                    atkStep =0;
                    golemAnim.Play("Golem_AtkC");
                    break;
            }
        }
    }

    void FreezeGolem()
    {
        enableAct = false;
    }
    void UnFreezeGolem()
    {
        enableAct = true;
    }
    void camDisAble()
    {
        Camera.SetActive(false);
    }
    void DestroyGolem()
    {     
        Destroy(this.gameObject);
    }

    void Activefalse() //���� ����� ī�޶� hp �� �ȳ����� ��Ȱ��ȭ ������
    {
        _BossHp.SetActive(false);
        _PlayerHp.SetActive(false);
    }
    void ActiveTrue() //��Ȱ��ȭ��Ų hp�� �ٽ� Ȱ��ȭ
    {
        _BossHp.SetActive(true);
        _PlayerHp.SetActive(true);
    }

    public void walksoundplay()
    {
        golemaudio.clip = walksound;
        golemaudio.Play();
    }
    public void attacksoundplay()
    {
        golemaudio.clip = attacksound;
        golemaudio.Play();
    }

}
