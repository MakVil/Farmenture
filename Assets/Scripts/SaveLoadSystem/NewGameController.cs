using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGameController : MonoBehaviour
{
    List<SaveSlot> saveSlots = new List<SaveSlot>();
    public static NewGameController Instance;

    public GameObject confirmWindow;
    public GameObject newGameWindow;

    private SaveSlot confirmSaveSlot;
    private SaveSlot newGameSaveSlot;

    public Button backButton;
    public GameObject saveNameInput;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PopulateSaveSlots();
    }

    public void PopulateSaveSlots()
    {
        saveSlots = new List<SaveSlot>();
        SaveSlot[] slots = gameObject.GetComponentsInChildren<SaveSlot>();

        foreach (SaveSlot slot in slots)
        {
            saveSlots.Add(slot);
            SaveLoadSystem.Instance.LoadSaveSlotData(slot);
            if(slot.saveName.text == null || slot.saveName.text.Length == 0)
                slot.deleteButton.gameObject.SetActive(false);
        }

        confirmWindow.SetActive(false);
        newGameWindow.SetActive(false);
    }

    public void SaveSlotClicked(SaveSlot saveSlot)
    {
        if (saveSlot.saveName.text != null && saveSlot.saveName.text.Length > 0)
        {
            confirmWindow.SetActive(true);

            SetButtonsEnabled(false);

            confirmSaveSlot = saveSlot;
        }
        else
        {
            ShowNewGameWindow(saveSlot);
        }
    }

    public void ConfirmOverrideClicked()
    {
        ShowNewGameWindow(confirmSaveSlot);
        confirmSaveSlot = null;
    }

    public void CancelOverrideClicked()
    {
        confirmSaveSlot = null;

        confirmWindow.SetActive(false);

        SetButtonsEnabled(true);
    }

    public void ShowNewGameWindow(SaveSlot saveSlot)
    {
        confirmWindow.SetActive(false);
        newGameWindow.SetActive(true);
        SetButtonsEnabled(false);

        newGameSaveSlot = saveSlot;
    }

    public void StartGameClicked()
    {
        string sName = GetSaveName();
        if(sName != null && sName.Length > 0)
        {
            StartNewGame(newGameSaveSlot);
            newGameSaveSlot = null;
        }
        else
        {
            Debug.Log("Invelid save name: " + sName);
        }
    }

    public void CancelNewGameClicked()
    {
        newGameSaveSlot = null;
        SetButtonsEnabled(true);
        newGameWindow.SetActive(false);
        SetSaveName(null);
    }

    public void StartNewGame(SaveSlot saveSlot)
    {
        SaveLoadSystem.saveName = GetSaveName();
        SaveLoadSystem.usedSaveSlot = saveSlot.saveSlot;
        SaveLoadSystem.loadSave = false;

        StartSceneController.Instance.StartGame();
    }

    public void SetButtonsEnabled(bool val)
    {
        foreach (SaveSlot slot in saveSlots)
        {
            Button button = slot.gameObject.GetComponent<Button>();
            button.enabled = val;
        }

        backButton.enabled = val;
    }

    public string GetSaveName()
    {
        string saveName = null;

        InputField sName = saveNameInput.GetComponent<InputField>();
        if(sName != null)
        {
            saveName = sName.text;
        }

        return saveName;
    }

    public void SetSaveName(string saveName)
    {
        InputField sName = saveNameInput.GetComponent<InputField>();
        if (sName != null)
        {
            sName.text = saveName;
        }
    }
}
