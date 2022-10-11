using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public List<ItemTable.RewardItem> items;
    [SerializeField]
    private Transform slotParent;
    [SerializeField]
    private Slot[] slots;
    public Transform ContentsArea;

    public TMPro.TMP_Text status;


    private void OnValidate()
    {
        slots = slotParent.GetComponentsInChildren<Slot>();
    }

    private void Awake()
    {
        status.text = "Hp : " + Player.hp_cur.ToString() + " / " + Player.hp.ToString()
                            + "  Mp : " + Player.Stamina_cur.ToString() + " / " + Player.stamina.ToString()
                            + "\n" + "Atk : " + Player.atk.ToString() + "      Def : " + Player.def.ToString();
        FreshSlot();
    }
    private void Update()
    {
        status.text = "Hp : " + Player.hp_cur.ToString() + " / " + Player.hp.ToString()
                            + "  Mp : " + Player.Stamina_cur.ToString() + " / " + Player.stamina.ToString()
                            + "\n" + "Atk : " + Player.atk.ToString() + "      Def : " + Player.def.ToString();
    }
    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slots.Length; i++)
        {
            slots[i].item = items[i];
        }
        for (; i < slots.Length; i++)
        {
            slots[i].item = null;
        }
    }

    public void AddItem(ItemTable.RewardItem _item)
    {
        if (items.Count < slots.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }
    }
    public void UseItem(ItemTable.RewardItem _item)
    {
        items.Remove(_item);
        FreshSlot();
    }

}

