using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirtPlotDataSaved", menuName = "Farming/Dirt plot")]
public class DirtPlotData : ScriptableObject
{
    private const string SAVE_KEY = "DirtPlotDataSave";

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
        foreach (Plant plant in plants.Values)
        {
            items.Add(plant.itemType.ToString());
            IDs.Add(plant.onDirtPlotID);
            days2Grows.Add(0);
        }
    }

    public void DeleteSave(int saveSlot)
    {
        if (PlayerPrefs.HasKey(SAVE_KEY + saveSlot.ToString()))
            PlayerPrefs.DeleteKey(SAVE_KEY + saveSlot.ToString());
    }
}
