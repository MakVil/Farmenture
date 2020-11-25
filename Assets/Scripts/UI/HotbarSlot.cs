using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : InventorySlot
{
    private static Color UNSELECTED_COLOR { get => new Color32(149, 149, 149, 255); }
    private static Color SELECTED_COLOR { get => new Color32(111, 111, 111, 255);}

    public bool isSelected;
    public Image itemBackground;

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if(isSelected)
        {
            itemBackground.color = SELECTED_COLOR;
        }
        else
        {
            itemBackground.color = UNSELECTED_COLOR;
        }
    }
}
