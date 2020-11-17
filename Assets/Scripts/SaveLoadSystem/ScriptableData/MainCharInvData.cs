using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCharInvDataSaved", menuName = "Main Character/Inventory")]
public class MainCharInvData : ScriptableObject
{
    private const string SAVE_KEY = "MainCharInvDataSave";

    private List<string> items = new List<string>();
    private List<int> counts = new List<int>();

    public void Save()
    {
        GetItemsAndCounts();

        string jsonData = JsonUtility.ToJson(this,true);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(PickUpTypeList list)
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY), this);
        for (int i = 0; i < counts.Count; i++)
        {
            if(list.GetPickUpItem(items[i]) != null)
                MainCharInventory.AddItemToInventory(list.GetPickUpItem(items[i]), counts[i]);
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
}
