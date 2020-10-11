using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarController : MonoBehaviour
{
    public Image mask;
    private float originalSize;

    public static EnergyBarController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
    }
    
    public void SetEnergyValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }

}
