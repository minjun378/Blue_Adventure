using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception : MonoBehaviour
{
    public UnityAction FindTarget = null;
    public LayerMask myEnemyMask;
    public GameObject myTarget = null;
    public List<GameObject> myEnemylist = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if ((myEnemyMask & (1 << other.gameObject.layer)) != 0)
        {
            myEnemylist.Add(other.gameObject);
            if (myTarget == null)
            {
                myTarget = other.gameObject;
                FindTarget?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        myEnemylist.Remove(other.gameObject);
    }
}
