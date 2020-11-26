using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingController : MonoBehaviour
{
    public List<DirtPlot> dirtPlotList;
    public List<DirtPlot> DirtPlotList { get => dirtPlotList; }
    public Dictionary<int, Plant> plantsOnDirtPlots;

    public static FarmingController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        dirtPlotList = new List<DirtPlot>();
        plantsOnDirtPlots = new Dictionary<int, Plant>();

        int ID = 0;
        DirtPlot[] plots = GetComponentsInChildren<DirtPlot>();
        foreach (DirtPlot plot in plots)
        {
            plot.ID = ID;
            plot.plant = PickUpItem.ItemTypes.Empty;
            dirtPlotList.Add(plot);

            ID++;
        }
    }

    public DirtPlot GetDirtPlot(int ID)
    {
        DirtPlot outPlot = null;

        foreach (DirtPlot plot in dirtPlotList)
        {
            if (plot.ID == ID)
            {
                outPlot = plot;
                break;
            }
        }

        return outPlot;
    }

    public void AgePlants()
    {
        foreach (DirtPlot plot in dirtPlotList)
        {
            plot.AgePlant();
        }
    }

    public void PickUpPlant(Plant pickUpPlant)
    {
        plantsOnDirtPlots.Remove(pickUpPlant.onDirtPlotID);
    } 

    public void AddPlant(int ID, Plant plant)
    {
        if (!plantsOnDirtPlots.ContainsKey(ID))
        {
            plantsOnDirtPlots.Add(ID, plant);
            plant.onDirtPlotID = ID;
        }
    }
    
    public void AddPlant(DirtPlot plot, Plant plant)
    {
        if(!plantsOnDirtPlots.ContainsKey(plot.ID))
        {
            int ID = plot.ID;
            plantsOnDirtPlots.Add(ID, plant);
            plant.onDirtPlotID = ID;
            plot.SpawnPlant(plant.prefab);
        }
    }
}
