using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

public class Monster : Battle_System
{
    public Animator playerAnim;
    public TextMeshProUGUI playerstate;

    public static float Speed = 4.0f;

    public bool enableAct;
    public float dist;
    public GameObject itemSphere; //아이템 원형

    public bool isDead = false;

    bool delay = false;

    public GameObject hitbox;

    [Serializable]
    public struct CharacterStat
    {
        public float ATK;
        public float DEF;
        public float HP;
        public float HP_cur;
    }
    
    public void Rotate(GameObject myTarget)
    {
        if (!isDead)
        {
            Vector3 dir = myTarget.transform.position - transform.position;
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
        }
    }

    public void Move(GameObject myTarget, Animator myAnim,float dist = 3)
    {
        if (!isDead)
        {
            if ((myTarget.transform.position - transform.position).magnitude >= dist)
            {
                myAnim.SetBool("IsMoving", true);
                transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
            }
            if ((myTarget.transform.position - transform.position).magnitude < dist)
            {
                myAnim.SetBool("IsMoving", false);
            }
        }
    }

    public void Attack(GameObject myTarget, Animator myAnim,float dist )
    {
        if (!isDead)
        {
            if ((myTarget.transform.position - transform.position).magnitude < dist && !delay)
            {
                delay = true;
                myAnim.Play("Attack01");
                StartCoroutine(isdelay());
            }
        }
    }

    public void Dead()
    {
        GameObject itemobj1 = Instantiate(itemSphere, this.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        GameObject itemobj2 = Instantiate(itemSphere, this.transform.position + new Vector3(0, 5, 0), Quaternion.identity);
        Destroy(this.gameObject);
    }



    IEnumerator isdelay(float t = 2.0f)
    {
        yield return new WaitForSeconds(t);
        delay = false;
    }
}

