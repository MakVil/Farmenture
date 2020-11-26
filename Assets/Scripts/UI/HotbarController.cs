using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour
{
    private List<HotbarSlot> hotbarSlotList = new List<HotbarSlot>();
    public List<HotbarSlot> HotbarSlotsList { get => hotbarSlotList; }
    public static HotbarController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        if (hotbarSlotList.Count == 0)
            InitSlotList();

        HotbarSlot slot0 = GetSlot(0);
        slot0.SetSelected(true);
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            HotbarSlot slot = GetSelected();
            slot.SetSelected(false);
            if (slot.ID < 7)
                GetSlot(slot.ID + 1).SetSelected(true);
            else
                GetSlot(0).SetSelected(true);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            HotbarSlot slot = GetSelected();
            slot.SetSelected(false);
            if (slot.ID > 0)
                GetSlot(slot.ID - 1).SetSelected(true);
            else
                GetSlot(7).SetSelected(true);
        }
    }

    private void InitSlotList()
    {
        HotbarSlot[] slots = GetComponentsInChildren<HotbarSlot>();
        for (int i = 0; i < 8; i++)
        {
            foreach (HotbarSlot slot in slots)
            {
                if (slot.ID == i)
                {
                    Instance.hotbarSlotList.Add(slot);
                    break;
                }
            }
        }
    }

    public void Action()
    {
        HotbarSlot slot = GetSelected();
        if (slot != null && slot.Item != null)
            slot.Item.Action();
    }

    public void AddItem(PickUpItem item, int toID)
    {
        this.AddItem(item, toID, -1);
    }

    public void AddItem(PickUpItem item, int toID, int count)
    {
        HotbarSlot slot = GetSlot(toID);
        if (slot != null)
        {
            if (slot.Item == null)
            {
                slot.AddItem(item, count);
            }
            else
                Debug.Log("hotbar item do not match with inventory item!");
        }
        else
            Debug.Log("hotbar slot not found!");

    }

    public void DecreaseItemCount(PickUpItem item, int count)
    {
        HotbarSlot slot = GetSlot(item.itemType);
        if (slot != null)
        {
            slot.UpdateCount(-count);
        }
        else
            Debug.Log("Hotbar slot not found!");
    }

    public void AddItemCount(PickUpItem item, int toID)
    {
        HotbarSlot slot = GetSlot(toID);
        if (slot != null)
        {
            if (slot.Item.itemType.Equals(item.itemType))
            {
                slot.UpdateCount(item.Count);
            }
            else
                Debug.Log("Hotbar item do not match with inventory item!");
        }
        else
            Debug.Log("Hotbar slot not found!");
    }

    private HotbarSlot GetSlot(int ID)
    {
        HotbarSlot outSlot = null;

        foreach (HotbarSlot slot in HotbarSlotsList)
        {
            if (slot.ID == ID)
            {
                outSlot = slot;
                break;
            }
        }

        return outSlot;
    }

    private HotbarSlot GetSlot(PickUpItem.ItemTypes itemType)
    {
        HotbarSlot outSlot = null;

        foreach (HotbarSlot slot in HotbarSlotsList)
        {
            if (slot.Item != null && slot.Item.itemType.Equals(itemType))
            {
                outSlot = slot;
                break;
            }
        }

        return outSlot;
    }

    private HotbarSlot GetSelected()
    {
        HotbarSlot outSlot = null;

        foreach (HotbarSlot slot in HotbarSlotsList)
        {
            if (slot.isSelected)
            {
                outSlot = slot;
                break;
            }
        }

        return outSlot;
    }

    public PickUpItem GetSelectedItem()
    {
        PickUpItem outItem = null;

        foreach (HotbarSlot slot in HotbarSlotsList)
        {
            if (slot.isSelected)
            {
                outItem = slot.Item;
                break;
            }
        }

        return outItem;
    }

    public Seed GetSelectedSeed()
    {
        PickUpItem item = GetSelectedItem();

        return (item is Seed ? (Seed) item : null);
    }

    public bool IsSeedSelected()
    {
        bool isSelected = false;

        HotbarSlot slot = GetSelected();
        if (slot != null && slot.Item != null)
        {
            isSelected = (slot.Item is Seed);
        }

        return isSelected;
    }

    public void EmptyHotbar()
    {
        if (hotbarSlotList == null || hotbarSlotList.Count == 0)
            InitSlotList();

        foreach (HotbarSlot slot in hotbarSlotList)
        {
            slot.EmptySlot();
        }
    }

    public void HideHotbar()
    {
        gameObject.SetActive(false);
    }

    public void ShowHotbar()
    {
        gameObject.SetActive(true);
    }
}
