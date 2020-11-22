using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public MainCharacterStats mainCharacterStats;

    public MainCharInvData mcInventoryData;

    public DirtPlotData dirtPlotData;

    public static int usedSaveSlot;
    public static string saveName;
    public static bool loadSave;

    public static SaveLoadSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (saveName != null && (usedSaveSlot == 1 || usedSaveSlot == 2 || usedSaveSlot ==3))
        {
            if (loadSave)
                LoadProgress();
            else
            {
                // Case of new game
                SaveProgress();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveProgress();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            LoadProgress();
        }
    }

    public void SaveProgress()
    {
        Debug.Log("Save data");
        mainCharacterStats.Save(usedSaveSlot, saveName);
        mcInventoryData.Save(usedSaveSlot);
        dirtPlotData.Save(usedSaveSlot);
    }

    public void LoadProgress()
    {
        Debug.Log("Load data");
        mainCharacterStats.Load(usedSaveSlot);
        mcInventoryData.Load(usedSaveSlot);
        dirtPlotData.Load(usedSaveSlot);
    }

    public void LoadSaveSlotData(SaveSlot slot)
    {
        mainCharacterStats.LoadSaveSlotData(slot);
    }

    public void DeleteSaveSlot(SaveSlot slot)
    {
        if (slot != null)
        {
            mainCharacterStats.DeleteSave(slot.saveSlot);
            mcInventoryData.DeleteSave(slot.saveSlot);
            dirtPlotData.DeleteSave(slot.saveSlot);
        }
    }
}
