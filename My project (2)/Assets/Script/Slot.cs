using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject Equipbutton;
    public TMPro.TMP_Text use_or_equip;
    public GameObject highlight;
    public TMPro.TMP_Text cnt;
    [SerializeField] Image image;
    public ItemTable.RewardItem _item;
    public Inventory inven;
    [SerializeField] GameObject inven_obj;
    [SerializeField] GameObject Head;
    [SerializeField] GameObject Amor;
    [SerializeField] GameObject Pants;
    [SerializeField] GameObject Shose;
    [SerializeField] GameObject Sword;
    [SerializeField] GameObject Ring;

    public Sprite Basic_Head;
    public Sprite Basic_Amor;
    public Sprite Basic_Pants;
    public Sprite Basic_Shose;
    public Sprite Basic_Sword;
    public Sprite Basic_Ring;

    public GameObject Is_Equip; //장착되면 E가 나오게끔 


    bool select = false;

    static Queue<GameObject> ItemButtonvisible = new Queue<GameObject>();

    public ItemTable.RewardItem item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.sprite;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.sprite = null;
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
    Vector2 DragOffSet = Vector2.zero;
    Transform Curparent = null;

    private void Awake()
    {
        inven = GameObject.Find("Inventory").GetComponent<Inventory>();
        inven_obj = GameObject.Find("Inventory").transform.Find("Equip").gameObject;

        Head = inven_obj.transform.Find("Head Slot").Find("Head_Equip").gameObject;
        Amor = inven_obj.transform.Find("Amor Slot").Find("Amor_Equip").gameObject;
        Pants = inven_obj.transform.Find("Pants Slot").Find("Pants_Equip").gameObject;
        Shose = inven_obj.transform.Find("Shose Slot").Find("Shose_Equip").gameObject;
        Sword = inven_obj.transform.Find("Sword Slot").Find("Sword_Equip").gameObject;
        Ring = inven_obj.transform.Find("Ring Slot").Find("Ring_Equip").gameObject;
    }
    private void Update()
    {
        if (this.image.sprite != null)
        {
            if (item.EquipCheck == ItemTable.RewardItem.equipcheck.equipTrue)
            {
                Is_Equip.SetActive(true);
            }
        }
        else if (this.image.sprite == null || item.EquipCheck == ItemTable.RewardItem.equipcheck.equipFalse)
        {
            Is_Equip.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1) && select && this.image.sprite != null)
        {
            Equipbutton.SetActive(true);
            ItemButtonvisible.Enqueue(Equipbutton);
            if (ItemButtonvisible.Count > 1)
            {
                ItemButtonvisible.Peek().SetActive(false);
                ItemButtonvisible.Dequeue();
            }

            if (item.Type == ItemTable.RewardItem.type.ect)
            {
                use_or_equip.text = "사용";
            }
            else
            {
                if (item.EquipCheck == ItemTable.RewardItem.equipcheck.equipFalse)
                {
                    use_or_equip.text = "장착";
                }
                else if (item.EquipCheck == ItemTable.RewardItem.equipcheck.equipTrue)
                {
                    use_or_equip.text = "해제";
                }
            }

        }
        
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().raycastTarget = false;
        Curparent = this.transform.parent;
        this.transform.SetParent(Curparent.parent); //부모의부모를 나의 부모로 만듦
        DragOffSet = (Vector2)this.transform.position - eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        this.transform.position = DragOffSet + eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<Image>().raycastTarget = true;
        this.transform.SetParent(Curparent);
        this.transform.localPosition = Vector3.zero;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        select = true;
        if(this.image.sprite != null) highlight.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        select = false;
        highlight.SetActive(false);
    }

    public void Exit()
    {
        Equipbutton.SetActive(false);
    }
    public void Select()
    {
        if (item.Type == ItemTable.RewardItem.type.ect)
        {
            use();
        }
        else
        {
            Equip();
        }
    }
    public void use()
    {
        if (item.Grade == ItemTable.RewardItem.grade.A)
        {
            Player.hp_cur += 700;
            if (Player.hp_cur >= Player.hp)
            {
                Player.hp_cur = Player.hp;
            }
        }
        else if (item.Grade == ItemTable.RewardItem.grade.C)
        {
            Player.hp_cur += 300;
            if (Player.hp_cur >= Player.hp)
            {
                Player.hp_cur = Player.hp;
            }
        } 
        inven.UseItem(item);
        Equipbutton.SetActive(false);
    }
    public void Equip()
    {
        Equipbutton.SetActive(false);
        if (item.EquipCheck == ItemTable.RewardItem.equipcheck.equipFalse)
        {          
            EquipItem(item.Grade, item.Type);
            item.EquipCheck = ItemTable.RewardItem.equipcheck.equipTrue;
        }
        else
        {
            item.EquipCheck = ItemTable.RewardItem.equipcheck.equipFalse;
            EquipItemCancle(item.Grade, item.Type);
        }
    }    
    public void EquipItem(ItemTable.RewardItem.grade grade, ItemTable.RewardItem.type type)
    {
        switch (type)
        {
            case ItemTable.RewardItem.type.sword:
                Player.atk += ItemGrade(grade)*2;
                Sword.GetComponent<Image>().sprite = this.image.sprite;
                Sword.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                break;
            case ItemTable.RewardItem.type.head:
                Head.GetComponent<Image>().sprite = this.image.sprite;
                Head.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Player.def += ItemGrade(grade);
                break;
            case ItemTable.RewardItem.type.pants:
                Pants.GetComponent<Image>().sprite = this.image.sprite;
                Pants.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Player.def += ItemGrade(grade);
                break;
            case ItemTable.RewardItem.type.ring:
                Ring.GetComponent<Image>().sprite = this.image.sprite;
                Ring.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Player.stamina += ItemGrade(grade);
                break;
            case ItemTable.RewardItem.type.shose:
                Shose.GetComponent<Image>().sprite = this.image.sprite;
                Shose.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Player.hp += ItemGrade(grade)*10;
                break;
            case ItemTable.RewardItem.type.amor:
                Amor.GetComponent<Image>().sprite = this.image.sprite;
                Amor.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Player.def += ItemGrade(grade)/2;
                Player.hp += ItemGrade(grade)*10;
                break;
        }
    }
    public void EquipItemCancle(ItemTable.RewardItem.grade grade, ItemTable.RewardItem.type type)
    {
        Is_Equip.SetActive(false);
        switch (type)
        {
            case ItemTable.RewardItem.type.sword:
                Sword.GetComponent<Image>().sprite = Basic_Sword;
                Sword.GetComponent<Image>().color = new Color(1,1,1,0.2f);
                Player.atk -= ItemGrade(grade) * 2;
                break;
            case ItemTable.RewardItem.type.head:
                Head.GetComponent<Image>().sprite = Basic_Head;
                Head.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                Player.def -= ItemGrade(grade);
                break;
            case ItemTable.RewardItem.type.pants:
                Pants.GetComponent<Image>().sprite = Basic_Pants;
                Pants.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                Player.def -= ItemGrade(grade);
                break;
            case ItemTable.RewardItem.type.ring:
                Ring.GetComponent<Image>().sprite = Basic_Ring;
                Ring.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                Player.stamina -= ItemGrade(grade);
                break;
            case ItemTable.RewardItem.type.shose:
                Shose.GetComponent<Image>().sprite = Basic_Shose;
                Shose.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                Player.hp -= ItemGrade(grade) * 10;
                break;
            case ItemTable.RewardItem.type.amor:
                Amor.GetComponent<Image>().sprite = Basic_Amor;
                Amor.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
                Player.def -= ItemGrade(grade) / 2;
                Player.hp -= ItemGrade(grade) * 10;
                break;
        }
    }
    public float ItemGrade(ItemTable.RewardItem.grade grade)
    {
        switch (grade)
        {
            case ItemTable.RewardItem.grade.A:
                return 50;
            case ItemTable.RewardItem.grade.B:
                return 30;
            case ItemTable.RewardItem.grade.C:
                return 10;
        }
        return 0;
    }
}

