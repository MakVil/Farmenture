using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameController : MonoBehaviour
{
    List<SaveSlot> saveSlots = new List<SaveSlot>();
    public static LoadGameController Instance;

    public Button backButton;

    private void Awake()
    {
        if (Instance == null)
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
            if (slot.saveName.text == null || slot.saveName.text.Length == 0)
            {
                Button button = slot.GetComponent<Button>();
                button.enabled = false;
                slot.deleteButton.gameObject.SetActive(false);
            }
            else
            {
                Button button = slot.GetComponent<Button>();
                button.enabled = true;
            }
        }
    }

    public void LoadSlotClicked(SaveSlot saveSlot)
    {
        SaveLoadSystem.saveName = saveSlot.saveName.text;
        SaveLoadSystem.usedSaveSlot = saveSlot.saveSlot;
        SaveLoadSystem.loadSave = true;
        SaveLoadSystem.loadTemp = false;

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
}
