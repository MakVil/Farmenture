using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCharacterStatsSaved", menuName = "Main Character/Stats")]
public class MainCharacterStats : ScriptableObject
{
    private const string SAVE_KEY = "MainCharacterStatSave";

    private int currentEnergy;

    public void Save(MainCharacterController mc)
    {
        if (mc != null)
        {
            currentEnergy = mc.Energy;

            string jsonData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(SAVE_KEY, jsonData);
            PlayerPrefs.Save();
        }
    }

    public void Load(MainCharacterController mc)
    {
        if (mc != null)
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY), this);

            mc.Energy = currentEnergy;
        }
    }
}
