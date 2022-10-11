using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item_3D : MonoBehaviour
{
    public float Speed = 15.0f;
    bool MoveStop = false;
    public ParticleSystem Light;
    static int cnt = 0;
    public ItemTable _item_table;
    public ItemTable.RewardItem ThisItem;

    // Start is called before the first frame update
    void Start()
    {
        ThisItem = _item_table.GetRandomItem();
        MoveStop = false;
        switch (ThisItem.Grade)
        {
            case ItemTable.RewardItem.grade.A:
                Light.startColor = Color.red;
                break;
            case ItemTable.RewardItem.grade.B:
                Light.startColor = Color.green;
                break;
            case ItemTable.RewardItem.grade.C:
                Light.startColor = Color.white;
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MoveStop)
        {
            switch (cnt = Random.Range(0,3))
            {
                case 0:
                    StartCoroutine(Moving(this.transform.position, this.transform.position + new Vector3(5, 0, 3)));                                  
                    break;
                case 1:
                    StartCoroutine(Moving(this.transform.position, this.transform.position + new Vector3(1, 0, -3)));
                    break;
                case 2:
                    StartCoroutine(Moving(this.transform.position, this.transform.position + new Vector3(-2, 0, 2)));
                    break;
            }            
            MoveStop = true;
        }
        
        
    }
    IEnumerator Moving(Vector3 src, Vector3 dest)
    {
        float dist = Vector3.Distance(src, dest);
        float s = 1.0f / (dist / Speed);
        float t = 0.0f;
        while (t < 1.0f)
        {
            Vector3 pos = Vector3.Lerp(src, dest, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * dist / 5;
            this.transform.position = pos;
            t += Time.deltaTime * s;
            yield return null;
        }
    }
}
