using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private PickUpItem item;
    public PickUpItem Item { get => item; }
    private int count;
    public int Count { get => count; }
    public Image itemCountBackground;
    public Image itemImage;
    public int ID;

    private void Start()
    {
        HideCountLabel();
        HideItemImage();
    }

    public void HideCountLabel()
    {
        if(itemCountBackground != null)
        {
            itemCountBackground.gameObject.SetActive(false);
        }
    }

    public void ShowCountLabel()
    {
        if (itemCountBackground != null)
        {
            itemCountBackground.gameObject.SetActive(true);
        }

    }

    public void HideItemImage()
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(false);
        }
    }

    public void ShowItemImage()
    {
        if (itemImage != null)
        {
            itemImage.gameObject.SetActive(true);
        }
    }

    public void UpdateCount(int amount)
    {
        count += amount;
        if(count > 1)
        {
            ShowCountLabel();
            TextMeshProUGUI text = itemCountBackground.GetComponentInChildren<TextMeshProUGUI>();
            if(text != null)
            {
                text.SetText(count.ToString());
            }
        }
        else if(count > 0)
        {
            HideCountLabel();
        }
        else
        {
            EmptySlot();
        }
    }

    public void EmptySlot()
    {
        item = null;
        count = 0;
        if(itemImage != null)
            itemImage.sprite = null;
        HideItemImage();
        HideCountLabel();
    }

    public void AddItem(PickUpItem newItem)
    {
        AddItem(newItem, -1);
    }

    public void AddItem(PickUpItem newItem, int itemCount)
    {
        if (itemCount < 0)
            count = newItem.Count;
        else
            count = itemCount;
        item = newItem;

        itemImage.sprite = item.itemSprite;
        itemImage.preserveAspect = true;
        ShowItemImage();
        UpdateCount(0);
        
    }

    public Sprite GetItemSprite()
    {
        if(item != null)
        {
            return item.itemSprite;
        }

        return null;
    }
}
