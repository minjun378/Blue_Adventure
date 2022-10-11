using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Slime : Monster
{
    public Animator myAnim;
    public LayerMask myEnemyMask;
    public GameObject myTarget = null;
    public Transform StartPos;


    public TMPro.TextMeshProUGUI dmgText;

    public Image Hp_Bar_Front;
    public Image Hp_Bar_Back;

    public GameObject myhp;
    public Camera maincamera;

    public enum STATE
    {
        NONE, CREAT, ROAMING, BATTLE, DEATH
    }
    public STATE myState = STATE.NONE;
    public CharacterStat myStat;

    public AudioSource attacksound;

    void FindTarget()
    {
        ChangeState(STATE.BATTLE);
    }
    void Start()
    {
        myStat.HP_cur = myStat.HP;
        ChangeState(STATE.CREAT);
        attacksound = GetComponent<AudioSource>();
    }
    void Update()
    {
        
        StateProcess();
        if (myTarget != null)
        {
            FindTarget();
        }
    }
    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case STATE.CREAT:
                StartPos = this.transform;
                ChangeState(STATE.ROAMING);
                break;
            case STATE.ROAMING:
                StartCoroutine(Roaming());
                break;
            case STATE.BATTLE:
                StopAllCoroutines();
                myhp.SetActive(true);
                break;
            case STATE.DEATH:
                hitbox.SetActive(false);
                isDead = true;
                myAnim.Play("Die");
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case STATE.CREAT:
                break;
            case STATE.ROAMING:
                break;
            case STATE.BATTLE:
                Rotate(myTarget);
                Move(myTarget,myAnim,3f);
                Attack(myTarget, myAnim,3f);
                SyncBar();
                myhp.transform.LookAt(maincamera.transform);
                if (myStat.HP_cur <= 0) ChangeState(STATE.DEATH);
                break;
            case STATE.DEATH:
                break;
        }
    }
    void SyncBar()
    {
        Hp_Bar_Front.fillAmount = myStat.HP_cur / myStat.HP;

        if (Hp_Bar_Back.fillAmount > Hp_Bar_Front.fillAmount)
        {
            Hp_Bar_Back.fillAmount = Mathf.Lerp(Hp_Bar_Back.fillAmount, Hp_Bar_Front.fillAmount, Time.deltaTime);
        }
    }

    public void AttackSound()
    {
        attacksound.Play();
    }
    IEnumerator Roaming()
    {
        Vector3 rndPos = new Vector3();
        rndPos.x = Random.Range(StartPos.position.x-10.0f, StartPos.position.x+10.0f);
        rndPos.z = Random.Range(StartPos.position.z- 10.0f, StartPos.position.z+10.0f);

        myAnim.SetBool("IsMoving", true);
        while (true)
        {
            Vector3 dir = (rndPos - transform.position).normalized;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
            transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);

            float Movedist = Vector3.Distance(transform.position,rndPos);
            if (Movedist <= 1f)
            {
                myAnim.SetBool("IsMoving", false);
                yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
                myAnim.SetBool("IsMoving", true);
                rndPos.x = Random.Range(StartPos.position.x - 10.0f, StartPos.position.x + 10.0f);
                rndPos.z = Random.Range(StartPos.position.z - 10.0f, StartPos.position.z + 10.0f);
            }
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemyMask & (1 << other.gameObject.layer)) != 0)
        {
            if (myTarget == null)
            {
                myTarget = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myTarget = null;
        ChangeState(STATE.ROAMING);
    }
    public void counting()
    {
        Player.slimeKill++;
    }
}
