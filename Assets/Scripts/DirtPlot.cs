using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPlot : MonoBehaviour
{
    public PickUpItem plant;
    public GameObject plantImg;
    
    public void PlantSeed(Seed seed)
    {
        plant = seed;
        SpriteRenderer ren = plantImg.GetComponent<SpriteRenderer>();
        if(ren != null)
            ren.sprite = seed.itemSprite;
        
        Debug.Log("Seed planted");
    }
}
