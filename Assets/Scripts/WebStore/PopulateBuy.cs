using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateBuy : MonoBehaviour
{
    public GameObject storeItemPrefab;
    public PickUpTypeList typeList;

    private List<StoreItem> storeItems = new List<StoreItem>();

    public static PopulateBuy Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Populate();
    }

    private void Populate()
    {
        GameObject obj;
        int playerMoney = MainCharacterController.Instance.Money;

        foreach (PickUpItem item in typeList.items)
        {
            if (item.canBeBought)
            {
                obj = Instantiate(storeItemPrefab, transform);
                StoreItem storeItem = obj.GetComponent<StoreItem>();
                if (storeItem != null)
                {
                    storeItem.itemImage.sprite = item.itemSprite;
                    storeItem.itemType = item.itemType;
                    storeItem.price = item.buyPrice;
                    storeItem.priceText.text = item.buyPrice.ToString();
                    storeItem.HideCountLabel();

                    if (playerMoney < item.buyPrice)
                    {
                        storeItem.gameObject.GetComponent<Button>().interactable = false;
                    }
                    storeItems.Add(storeItem);
                }
            }
        }
    }

    public void UpdateItems()
    {
        int playerMoney = MainCharacterController.Instance.Money;
        foreach (StoreItem item in storeItems)
        {
            if(playerMoney < item.price)
                item.gameObject.GetComponent<Button>().interactable = false;
            else
                item.gameObject.GetComponent<Button>().interactable = true;
        }
    }
}
