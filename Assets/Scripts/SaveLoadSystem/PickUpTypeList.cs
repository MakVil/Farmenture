using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTypeList : MonoBehaviour
{
    public PickUpItem[] items;

    public static PickUpTypeList Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public PickUpItem GetPickUpItem(string itemTypeStr)
    {
        if (itemTypeStr == null)
            return null;

        PickUpItem.ItemTypes type = PickUpItem.GetItemType(itemTypeStr);

        return GetPickUpItem(type);
    }

    public PickUpItem GetPickUpItem(PickUpItem.ItemTypes type)
    {
        PickUpItem outItem = null;

        foreach (PickUpItem item in items)
        {
            if (item.itemType.Equals(type))
            {
                outItem = item;
                break;
            }
        }

        return outItem;
    }
}
