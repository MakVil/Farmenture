using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Sprite itemSprite;
    public bool isUnique;
    public int count = 1;
    public int Count { get => count; }
    public int currentCount;
    public ItemTypes itemType;

    public int buyPrice;
    public int sellPrice;
    public bool canBeSelled = false;
    public bool canBeBought = false;

    public enum ItemTypes
    {
        Empty,
        GoldNugget,
        Axe,
        PumpkinSeed,
        Pumpkin,
        Meat,
        TaxiTicket,
        Aubergine,
        AubergineSeed,
        Strawberry,
        StrawberrySeed,
        Random
    }

    public static ItemTypes GetItemType(string type)
    {
        foreach (ItemTypes itemType in System.Enum.GetValues(typeof(ItemTypes)))
        {
            if (itemType.ToString().Equals(type))
            {
                return itemType;
            }
        }

        return ItemTypes.Random;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacterController controller = collision.GetComponent<MainCharacterController>();

        if (controller != null && MainCharInventory.Instance != null)
        {
            MainCharInventory.Instance.CollectItem(this);
        }
    }

    public override string ToString()
    {
        return itemType.ToString();
    }

    public void Action()
    {
        Debug.Log(itemType);
    }
}
