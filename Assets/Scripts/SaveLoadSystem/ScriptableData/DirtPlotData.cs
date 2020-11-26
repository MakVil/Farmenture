using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirtPlotDataSaved", menuName = "Farming/Dirt plot")]
public class DirtPlotData : ScriptableObject
{
    private const string SAVE_KEY = "DirtPlotDataSave";
    private const string TEMP_SAVE_KEY = "DirtPlotDataSaveTemp";

    public List<string> items = new List<string>();
    public List<int> IDs = new List<int>();
    public List<int> days2Grows = new List<int>();

    public void Save(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            GetData();

            string jsonData = JsonUtility.ToJson(this, true);
            PlayerPrefs.SetString(SAVE_KEY + saveSlot, jsonData);
            PlayerPrefs.Save();
            Debug.Log("dirt save " + jsonData);
        }
        else
        {
            Debug.Log("Save slot number invalid!");
        }
    }

    public void Load(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            PickUpTypeList list = PickUpTypeList.Instance;

            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY + saveSlot), this);
            for (int i = 0; i < IDs.Count; i++)
            {
                PickUpItem item = list.GetPickUpItem(items[i]);
                if (item != null)
                {
                    DirtPlot plot = FarmingController.Instance.GetDirtPlot(IDs[i]);
                    if (plot != null)
                    {
                        if (item is Plant)
                        {
                            FarmingController.Instance.AddPlant(plot, ((Plant) item));
                        }
                        else
                        {
                            plot.AddPlant(item, days2Grows[i]);
                        }
                    }
                }
            }
            Debug.Log("dirt load " + PlayerPrefs.GetString(TEMP_SAVE_KEY));
        }
        else
        {
            Debug.Log("Save slot number invalid!");
        }
    }

    public void SaveTemp(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            GetData();

            string jsonData = JsonUtility.ToJson(this, true);
            PlayerPrefs.SetString(TEMP_SAVE_KEY, jsonData);
            PlayerPrefs.Save();
            Debug.Log("Temp dirt save " + jsonData);
        }
        else
        {
            Debug.Log("Save slot number invalid!");
        }
    }

    public void LoadTemp(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            PickUpTypeList list = PickUpTypeList.Instance;

            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(TEMP_SAVE_KEY), this);
            for (int i = 0; i < IDs.Count; i++)
            {
                PickUpItem item = list.GetPickUpItem(items[i]);
                if (item != null)
                {
                    DirtPlot plot = FarmingController.Instance.GetDirtPlot(IDs[i]);
                    if (plot != null)
                    {
                        if (item is Plant)
                        {
                            FarmingController.Instance.AddPlant(plot, ((Plant) item));
                        }
                        else
                        {
                            plot.AddPlant(item, days2Grows[i]);
                        }
                    }
                    else
                    {
                        Debug.Log("Dirtplot not found! ID " + IDs[i]);
                    }
                }
            }
            Debug.Log("Temp dirt load " + PlayerPrefs.GetString(TEMP_SAVE_KEY));
        }
        else
        {
            Debug.Log("Save slot number invalid!");
        }
    }

    private void GetData()
    {
        items = new List<string>();
        IDs = new List<int>();
        days2Grows = new List<int>();

        List<DirtPlot> plots = FarmingController.Instance.DirtPlotList;
        foreach (DirtPlot plot in plots)
        {
            if (!plot.plant.Equals(PickUpItem.ItemTypes.Empty))
            {
                items.Add(plot.plant.ToString());
                IDs.Add(plot.ID);
                days2Grows.Add(plot.daysToGrow);
            }
        }

        Dictionary<int, Plant> plants = FarmingController.Instance.plantsOnDirtPlots;
        
        int[] IDKeys = new int[plants.Count];
        int i = 0;
        foreach(int ID in plants.Keys)
        {
            IDKeys[i] = ID;
            i++;
        }

        i = 0;
        foreach (Plant plant in plants.Values)
        {
            IDs.Add(IDKeys[i]);
            items.Add(plant.itemType.ToString());
            days2Grows.Add(0);

            i++;
        }
    }

    public void DeleteSave(int saveSlot)
    {
        if (PlayerPrefs.HasKey(SAVE_KEY + saveSlot.ToString()))
            PlayerPrefs.DeleteKey(SAVE_KEY + saveSlot.ToString());
    }
}
