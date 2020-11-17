using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DirtPlotDataSaved", menuName = "Farming/Dirt plot")]
public class DirtPlotData : ScriptableObject
{
    private const string SAVE_KEY = "DirtPlotDataSave";

    private List<string> items = new List<string>();
    private List<int> IDs = new List<int>();
    private List<int> days2Grows = new List<int>();

    public void Save()
    {
        GetData();

        string jsonData = JsonUtility.ToJson(this, true);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
        PlayerPrefs.Save();
    }

    public void Load(PickUpTypeList list)
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY), this);
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
}
