using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public GameObject mainCharacter;
    public MainCharacterStats mainCharacterStats;

    public MainCharInvData mcInventoryData;

    public GameObject pickUpTypeList;

    public static SaveLoadSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveProgress();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            LoadProgress();
        }
    }

    public void SaveProgress()
    {
        Debug.Log("Save data");
        mainCharacterStats.Save(mainCharacter.GetComponent<MainCharacterController>());
        mcInventoryData.Save();
    }

    public void LoadProgress()
    {
        Debug.Log("Load data");
        mainCharacterStats.Load(mainCharacter.GetComponent<MainCharacterController>());
        mcInventoryData.Load(pickUpTypeList.GetComponent<PickUpTypeList>());
    }

}
