using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Loot Table")]
public class ItemTable : ScriptableObject
{
    [System.Serializable]
    public class RewardItem
    {
        public string itemName;
        public float weight;
        public Sprite sprite;
        public enum type
        {
            sword, ring, amor, pants, head, shose, ect
        };
        public type Type;
        public enum grade
        {
            A,B,C
        };
        public grade Grade;
        public enum equipcheck
        {
            equipFalse , equipTrue
        };
        public equipcheck EquipCheck = equipcheck.equipFalse;
    }

    [SerializeField] public List<RewardItem> _items;

    [System.NonSerialized] public bool isInitialized = false;

    public float _totalWeight;
    private void Initialize()
    {
        if (!isInitialized)
        {
            _totalWeight = _items.Sum(item => item.weight);
            isInitialized = true;
        }
    }
    public RewardItem GetRandomItem()
    {
        Initialize();

        float diceRoll = Random.Range(0f, _totalWeight);

        foreach (var item in _items)
        {
            if (item.weight >= diceRoll)
            {
                return item;
            }
            diceRoll -= item.weight;
        }

        throw new System.Exception("Reward generation failed!");
    }
}


