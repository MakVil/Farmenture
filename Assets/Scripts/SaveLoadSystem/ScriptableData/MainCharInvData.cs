﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCharInvDataSaved", menuName = "Main Character/Inventory")]
public class MainCharInvData : ScriptableObject
{
    private const string SAVE_KEY = "MainCharInvDataSave";

    public List<string> items = new List<string>();
    public List<int> counts = new List<int>();

    public void Save(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            GetItemsAndCounts();

            string jsonData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(SAVE_KEY + saveSlot, jsonData);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Save slot number invalid!");
        }
    }

    public void Load(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            MainCharInventory.Instance.EmptyInventory();
            HotbarController.Instance.EmptyHotbar();

            PickUpTypeList list = PickUpTypeList.Instance;

            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY + saveSlot), this);
            for (int i = 0; i < counts.Count; i++)
            {
                if(list.GetPickUpItem(items[i]) != null)
                    MainCharInventory.Instance.AddItemToInventory(list.GetPickUpItem(items[i]), counts[i]);
                }
            }
        else
        {
            Debug.Log("Save slot number invalid!");
        }
    }

    private void GetItemsAndCounts()
    {
        items = new List<string>();
        counts = new List<int>();

        List<InventorySlot> slots = MainCharInventory.Instance.InventorySlotsList;
        foreach (InventorySlot slot in slots)
        {
            if (slot.Item != null)
            {
                items.Add(slot.Item.itemType.ToString());
                counts.Add(slot.Count);
            }
        }
    }

    public void DeleteSave(int saveSlot)
    {
        if (PlayerPrefs.HasKey(SAVE_KEY + saveSlot.ToString()))
            PlayerPrefs.DeleteKey(SAVE_KEY + saveSlot.ToString());
    }
}
