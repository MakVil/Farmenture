using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopulateSell : MonoBehaviour
{
    public GameObject storeItemPrefab;
    public PickUpTypeList typeList;

    private List<StoreItem> storeItems = new List<StoreItem>();

    public static PopulateSell Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        Populate();
    }

    public void Populate()
    {
        foreach (StoreItem item in storeItems)
        {
            Destroy(item.gameObject);
        }
        storeItems.Clear();

        GameObject obj;
        List<PickUpItem> items = MainCharInventory.Instance.GetItems();
        foreach (PickUpItem item in items)
        {
            if (item.canBeSelled)
            {
                obj = Instantiate(storeItemPrefab, transform);
                StoreItem storeItem = obj.GetComponent<StoreItem>();
                if (storeItem != null)
                {
                    storeItem.itemImage.sprite = item.itemSprite;
                    storeItem.itemType = item.itemType;
                    storeItem.price = item.sellPrice;
                    storeItem.priceText.text = item.sellPrice.ToString();
                    storeItem.count = item.currentCount;

                    storeItem.UpdateCount(0);

                    storeItems.Add(storeItem);
                }
            }
        }
    }

    public void RemoveItem(StoreItem item)
    {
        Destroy(item.gameObject);
        storeItems.Remove(item);
    }
    
}
