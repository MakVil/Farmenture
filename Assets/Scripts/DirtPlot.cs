using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPlot : MonoBehaviour
{
    public PickUpItem.ItemTypes plant = PickUpItem.ItemTypes.Empty;
    public GameObject plantImg;
    public int ID;

    public int daysToGrow;

    public void PlantSeed(Seed seed)
    {
        if (plant.Equals(PickUpItem.ItemTypes.Empty))
        {
            plant = seed.itemType;

            daysToGrow = seed.daysToGrow;

            SpriteRenderer ren = plantImg.GetComponent<SpriteRenderer>();
            if (ren != null)
                ren.sprite = seed.itemSprite;

            MainCharInventory.Instance.DecreaseItemCount(seed, 1);
        }
    }

    // Used when loading data
    public void AddPlant(PickUpItem item, int d2g)
    {
        plant = item.itemType;
        daysToGrow = d2g;

        SpriteRenderer ren = plantImg.GetComponent<SpriteRenderer>();
        if (ren != null)
            ren.sprite = item.itemSprite;
    }

    public void AgePlant()
    {
        daysToGrow = Mathf.Clamp(daysToGrow - 1, 0, 1000);

        if (daysToGrow == 0)
        {
            // Check if the plant is seed. If seed, grow it to plant
            PickUpItem plantItem = SaveLoadSystem.Instance.pickUpTypeList.GetPickUpItem(plant);
            if (plantItem is Seed)
            {
                GameObject newPlantOb = SpawnPlant(((Seed) plantItem).growToPrefab);
                Plant p = (Plant) newPlantOb.GetComponent<PickUpItem>();
                p.onDirtPlotID = ID;
                FarmingController.Instance.AddPlant(ID, p);

                plant = PickUpItem.ItemTypes.Empty;
                daysToGrow = 0;

                SpriteRenderer ren = plantImg.GetComponent<SpriteRenderer>();
                if (ren != null)
                    ren.sprite = null;
            }
        }

        else
            Debug.Log("Plant grows! Days left " + daysToGrow);
    }

    public GameObject SpawnPlant(GameObject plantPrefab)
    {
        return Instantiate(plantPrefab, transform.position, Quaternion.identity);
    }
}
