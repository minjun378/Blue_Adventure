using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchItem : MonoBehaviour
{
    public Queue<GameObject> search_item_list = new Queue<GameObject>();
    public Inventory inven;
    public GameObject Get_Item;
    public Transform NoticeBoard;
    public List<GameObject> Itemname_Notice = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && search_item_list.Count != 0)
        {
            GameObject itemname = Instantiate(Get_Item,NoticeBoard);
            itemname.GetComponentInChildren<TMPro.TMP_Text>().text 
                = search_item_list.Peek().GetComponent<Item_3D>().ThisItem.itemName.ToString() + " È¹µæ";
            Itemname_Notice.Add(itemname);
            inven.AddItem(search_item_list.Peek().GetComponent<Item_3D>().ThisItem);
            Destroy(search_item_list.Dequeue());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            search_item_list.Enqueue(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            search_item_list.Dequeue();
        }
    }

}
