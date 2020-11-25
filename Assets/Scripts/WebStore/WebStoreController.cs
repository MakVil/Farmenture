using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebStoreController : MonoBehaviour
{
    public static WebStoreController Instance { get; private set; }

    public GameObject mainView;
    public GameObject buyView;
    public GameObject sellView;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        HideWebStore();
    }

    public void ClickBuy()
    {
        mainView.SetActive(false);
        buyView.SetActive(true);

        PopulateBuy.Instance.UpdateItems();
    }

    public void ClickSell()
    {
        mainView.SetActive(false);
        sellView.SetActive(true);

        PopulateSell.Instance.Populate();
    }

    public void ClickBack()
    {
        mainView.SetActive(true);
        buyView.SetActive(false);
        sellView.SetActive(false);
    }

    public void CloseStore()
    {
        UIController.Instance.CloseWebStore();
    }

    public void HideWebStore()
    {
        gameObject.SetActive(false);
        buyView.SetActive(false);
        sellView.SetActive(false);
    }

    public void ShowWebStore()
    {
        gameObject.SetActive(true);

        mainView.SetActive(true);
        buyView.SetActive(false);
        sellView.SetActive(false);
    }
}
