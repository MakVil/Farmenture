using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainCharacterStatsSaved", menuName = "Main Character/Stats")]
public class MainCharacterStats : ScriptableObject
{
    private const string SAVE_KEY = "MainCharacterStatSave";

    public int currentEnergy;
    public int currentMoney;
    public string saveName;
    public int day;

    public void Save(int saveSlot, string sName)
    {
        if ( saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            MainCharacterController mc = MainCharacterController.Instance;
            currentEnergy = mc.Energy;
            currentMoney = mc.Money;
            saveName = sName;
            day = TimeSystem.Instance.day;

            string jsonData = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(SAVE_KEY + saveSlot.ToString(), jsonData);
            PlayerPrefs.Save();

            EmptyFields();
        }
        else
        {
            Debug.Log("Save slot is invalid!");
        }
    }

    public void Load(int saveSlot)
    {
        if (saveSlot == 1 || saveSlot == 2 || saveSlot == 3)
        {
            MainCharacterController mc = MainCharacterController.Instance;
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY + saveSlot.ToString()), this);

            mc.Energy = currentEnergy;
            mc.Money = currentMoney;
            TimeSystem.Instance.day = day;

            EmptyFields();
        }
        else
        {
            Debug.Log("Save slot is invalid!");
        }
    }

    public void LoadSaveSlotData(SaveSlot slot)
    {
        if(slot.saveSlot == 1 || slot.saveSlot == 2 || slot.saveSlot == 3)
        {            
            MainCharacterController mc = MainCharacterController.Instance;
            
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SAVE_KEY + slot.saveSlot.ToString()), this);
            
            slot.SetCoins(currentMoney.ToString());
            slot.SetDays(day.ToString());
            slot.SetSaveName(saveName);

            EmptyFields();
        }
        else
        {
            Debug.Log("Save slot is invalid!");
        }
    }

    public void EmptyFields()
    {
        currentMoney = 0;
        currentEnergy = 0;
        day = 0;
        saveName = null;
    }

    public void DeleteSave(int saveSlot)
    {
        if (PlayerPrefs.HasKey(SAVE_KEY + saveSlot.ToString()))
            PlayerPrefs.DeleteKey(SAVE_KEY + saveSlot.ToString());
    }
}
