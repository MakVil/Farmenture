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
            {
                LoadTempProgress();
                if(SceneManager.GetActiveScene().name.Equals("FarmScene"))
                {
                    LoadTempDirtProgress();
                }
            }
            else if (TimelineController.Instance != null && TimelineController.Instance.playableDirector != null)
            {
                // Case of new game
                SaveProgress();
                TimelineController.Instance.playableDirector.Play();
                WebStoreController.firstBuyOpen = true;
            }

            // Case of going into sleep while in forest
            if (showSaveInfo)
            {
                LoadTempDirtProgress();
                LoadTempProgress();
                // Counter what is done in LoadTempProgress()
                MainCharacterController.Instance.MoveToStartPosition();

                FarmingController.Instance.AgePlants();
                MainCharacterController.Instance.RefillEnergy();

                SaveLoadSystem.Instance.SaveProgress();

                BedController.Instance.SetToSleepSprite();
                NightController.Instance.EndNightAfterForest();
                showSaveInfo = false;
            }
        }
    }

    public void SaveProgress()
    {
        mainCharacterStats.Save(usedSaveSlot, saveName);
        mcInventoryData.Save(usedSaveSlot);
        dirtPlotData.Save(usedSaveSlot);
    }

    public void SaveTempProgress()
    {
        mainCharacterStats.SaveTemp(usedSaveSlot, saveName);
        mcInventoryData.SaveTemp(usedSaveSlot);
    }

    public void SaveTempDirtProgress()
    {
        dirtPlotData.SaveTemp(usedSaveSlot);
    }

    public void LoadProgress()
    {
        mainCharacterStats.Load(usedSaveSlot);
        mcInventoryData.Load(usedSaveSlot);
        dirtPlotData.Load(usedSaveSlot);
    }

    public void LoadTempProgress()
    {
        mainCharacterStats.LoadTemp(usedSaveSlot);
        mcInventoryData.LoadTemp(usedSaveSlot);

        if(SceneManager.GetActiveScene().name.Equals("FarmScene"))
        {
            MainCharacterController.Instance.MoveToForestEntry();
        }
    }

    public void LoadTempDirtProgress()
    {
        dirtPlotData.LoadTemp(usedSaveSlot);
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
