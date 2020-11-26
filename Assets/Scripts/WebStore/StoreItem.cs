using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour
{
    public Image itemImage;
    public Image itemCountBackground;
    public TextMeshProUGUI priceText;
    public int price;
    public int count;
    public PickUpItem.ItemTypes itemType;

    public void BuyItem()
    {
        if (itemType != PickUpItem.ItemTypes.TaxiTicket)
        {
            MainCharacterController.Instance.ReduceMoney(price);
            PickUpItem item = PickUpTypeList.Instance.GetPickUpItem(itemType);
            MainCharInventory.Instance.CollectItem(item);
            PopulateBuy.Instance.UpdateItems();
        }
        else
        {
            UIController.Instance.OpenEndGameMenu();
        }
    }

    public void SellItem()
    {
        MainCharacterController.Instance.AddMoney(price);
        PickUpItem item = PickUpTypeList.Instance.GetPickUpItem(itemType);
        MainCharInventory.Instance.DecreaseItemCount(item, 1);
        UpdateCount(-1);
    }

    public void UpdateCount(int amount)
    {
        count += amount;
        if (count > 1)
        {
            ShowCountLabel();
            TextMeshProUGUI text = itemCountBackground.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.SetText(count.ToString());
            }
        }
        else if (count > 0)
        {
            HideCountLabel();
        }
        else
        {
            PopulateSell.Instance.RemoveItem(this);
        }
    }

    public void HideCountLabel()
    {
        if (itemCountBackground != null)
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
}
