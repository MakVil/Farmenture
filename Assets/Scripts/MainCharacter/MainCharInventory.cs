using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharInventory : MonoBehaviour
{
    private List<InventorySlot> inventorySlotList;
    public List<InventorySlot> InventorySlotsList { get => inventorySlotList; }
    public static MainCharInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Instance.inventorySlotList = new List<InventorySlot>();
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < 32; i++)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.ID == i)
                {
                    Instance.inventorySlotList.Add(slot);
                    break;
                }
            }
        }

        gameObject.SetActive(false);
    }

    public void CollectItem(PickUpItem item)
    {
        if (item != null && item.itemSprite != null)
        {
            Sprite sprite = item.itemSprite;
            InventorySlot emptySlot = null;
            bool removeItem = false;
            bool itemCollected = false;

            // First check if the item is already in the inventory and find empty slot just in case
            foreach (InventorySlot slot in inventorySlotList)
            {
                if (slot.GetItemSprite() != null && slot.GetItemSprite().Equals(sprite))
                {
                    if (!item.isUnique)
                    {
                        slot.UpdateCount(item.Count);
                        removeItem = true;

                        // Update hotbar
                        if(slot.ID >= 0 || slot.ID < 8)
                        {
                            HotbarController.Instance.AddItemCount(item, slot.ID);
                        }
                    }
                    itemCollected = true;
                    break;
                }
                if (slot.GetItemSprite() == null && emptySlot == null)
                {
                    emptySlot = slot;
                }
            }

            // If item was not added to existing slot and there is empty slot, add it to new slot
            if (!itemCollected && emptySlot != null)
            {
                emptySlot.AddItem(item);
                removeItem = true;

                // Update hotbar
                if (emptySlot.ID >= 0 || emptySlot.ID < 8)
                {
                    HotbarController.Instance.AddItem(item, emptySlot.ID);
                }
            }

            // Remove item from world if it was picked up
            if (removeItem)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Unique item already owned or inventory full");
            }
        }
    }

    // Used for adding items when loading saved data
    public static void AddItemToInventory(PickUpItem item, int count)
    {
        foreach (InventorySlot slot in Instance.inventorySlotList)
        {
            if (slot.GetItemSprite() == null)
            {
                slot.AddItem(item, count);

                // Update hotbar
                if (slot.ID >= 0 || slot.ID < 8)
                {
                    HotbarController.Instance.AddItem(item, slot.ID, count);
                }
                break;
            }
        }
    }
}
