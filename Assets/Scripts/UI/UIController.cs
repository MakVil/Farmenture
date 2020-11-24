using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameObject coinTextBox;
    public TextMeshProUGUI coinText;

    public GameObject EnergyBox;
    private MainCharInventory mainCharInv;
    private MainCharacterController mainCharCont;
    private HotbarController hotbarCont;
    private DialogController dialogCont;
    private WebStoreController storeCont;
    private SettingsController settingsCont;
    private NightController nightCont;
    private TimeSystem timeSystem;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coinTextBox.SetActive(false);

        mainCharInv = MainCharInventory.Instance;
        hotbarCont = HotbarController.Instance;
        dialogCont = DialogController.Instance;
        storeCont = WebStoreController.Instance;
        mainCharCont = MainCharacterController.Instance;
        settingsCont = SettingsController.Instance;
        nightCont = NightController.Instance;
        timeSystem = TimeSystem.Instance;
    }

    public void CloseInventory()
    {
        mainCharInv.gameObject.SetActive(false);
        hotbarCont.gameObject.SetActive(true);

        coinTextBox.gameObject.SetActive(false);
        timeSystem.ShowTimeBox();
        EnergyBox.gameObject.SetActive(true);

        mainCharCont.canMove = true;
    }

    public void OpenInventory()
    {
        if (!storeCont.gameObject.activeSelf && !dialogCont.gameObject.activeSelf)
        {
            mainCharInv.gameObject.SetActive(true);
            hotbarCont.gameObject.SetActive(false);

            coinTextBox.gameObject.SetActive(true);
            UpdateMoney();
            timeSystem.HideTimeBox();

            EnergyBox.gameObject.SetActive(false);

            mainCharCont.canMove = false;
        }
    }

    public void CloseWebStore()
    {
        storeCont.HideWebStore();
        hotbarCont.ShowHotbar();

        coinTextBox.gameObject.SetActive(false);
        EnergyBox.gameObject.SetActive(true);
        timeSystem.ShowTimeBox();

        EnergyBox.gameObject.SetActive(true);

        mainCharCont.canMove = true;
    }

    public void OpenWebStore()
    {
        storeCont.ShowWebStore();
        hotbarCont.HideHotbar();

        coinTextBox.gameObject.SetActive(true);
        UpdateMoney();
        timeSystem.HideTimeBox();

        EnergyBox.gameObject.SetActive(false);

        mainCharCont.canMove = false;
    }

    public void CloseDialog()
    {
        dialogCont.CloseDialog();
        hotbarCont.ShowHotbar();
    }

    public void OpenDialog()
    {
        dialogCont.OpenDialog();
        hotbarCont.HideHotbar();
    }

    public void OpenSettings()
    {
        settingsCont.OpenSettings();

        CloseDialog();
        CloseInventory();
        CloseWebStore();

        mainCharCont.canMove = false;
    }

    public void CloseSettings()
    {
        settingsCont.CloseSettings();
        mainCharCont.canMove = true;
    }

    public void EndDay()
    {
        CloseAll();
        nightCont.StartNight();
    }

    public void CloseAll()
    {
        CloseDialog();
        CloseInventory();
        CloseWebStore();

        timeSystem.ShowTimeBox();

        mainCharCont.canMove = true;
    }

    public bool IsAllClosed()
    {
        return !dialogCont.gameObject.activeSelf && 
            !mainCharInv.gameObject.activeSelf && 
            !storeCont.gameObject.activeSelf;
    }

    public void UpdateMoney()
    {
        coinText.SetText(mainCharCont.Money.ToString());
    }
}
