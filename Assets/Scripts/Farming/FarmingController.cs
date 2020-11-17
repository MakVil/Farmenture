using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingController : MonoBehaviour
{
    private List<DirtPlot> dirtPlotList;

    void Start()
    {
        dirtPlotList = new List<DirtPlot>();

        DirtPlot[] plots = GetComponentsInChildren<DirtPlot>();
        foreach (DirtPlot plot in plots)
        {
            dirtPlotList.Add(plot);
        }
    }
    
}
