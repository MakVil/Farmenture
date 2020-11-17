using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTypeList : MonoBehaviour
{
    public PickUpItem[] items;

    private void Start()
    {
    }

    public PickUpItem GetPickUpItem(string itemTypeStr)
    {
         PickUpItem outItem = null;
         PickUpItem.ItemTypes type = PickUpItem.GetItemType(itemTypeStr);

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
