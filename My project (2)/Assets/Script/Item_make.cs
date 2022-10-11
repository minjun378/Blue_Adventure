using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_make : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public float weight;
    public enum type
    {
        sword,ring,amor,pants,head,shose,ect
    };
    public type types;
}

