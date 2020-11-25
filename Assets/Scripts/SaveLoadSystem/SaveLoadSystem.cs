using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadSystem : MonoBehaviour
{
    public MainCharacterStats mainCharacterStats;

    public MainCharInvData mcInventoryData;

    public DirtPlotData dirtPlotData;

    public static int usedSaveSlot;
    public static string saveName;
    public static bool loadSave;
    public static bool loadTemp;
    public static bool showSaveInfo;

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
            else if (loadTemp)
                LoadTempProgress();
            else
            {
                // Case of new game
                SaveProgress();
                TimelineController.Instance.playableDirector.Play();
            }

            // Case of going into sleep while in forest
            if (showSaveInfo)
            {
                BedController.Instance.SetToSleepSprite();
                NightController.Instance.EndNightAfterForest();
                showSaveInfo = false;
            }
        }
    }

    public void SaveProgress()
    {
        Debug.Log("Save data");
        mainCharacterStats.Save(usedSaveSlot, saveName);
        mcInventoryData.Save(usedSaveSlot);
        dirtPlotData.Save(usedSaveSlot);
    }

    public void SaveTempProgress()
    {
        Debug.Log("Save data");
        mainCharacterStats.SaveTemp(usedSaveSlot, saveName);
        mcInventoryData.SaveTemp(usedSaveSlot);
        dirtPlotData.SaveTemp(usedSaveSlot);
    }

    public void LoadProgress()
    {
        Debug.Log("Load data");
        mainCharacterStats.Load(usedSaveSlot);
        mcInventoryData.Load(usedSaveSlot);
        dirtPlotData.Load(usedSaveSlot);
    }

    public void LoadTempProgress()
    {
        Debug.Log("Load data");
        mainCharacterStats.LoadTemp(usedSaveSlot);
        mcInventoryData.LoadTemp(usedSaveSlot);
        dirtPlotData.LoadTemp(usedSaveSlot);

        if(SceneManager.GetActiveScene().name.Equals("FarmScene"))
        {
            MainCharacterController.Instance.MoveToForestEntry();
        }
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
