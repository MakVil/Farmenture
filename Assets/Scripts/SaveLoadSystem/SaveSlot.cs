using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public int saveSlot;
    public Text saveName;
    public Text days;
    public Text coins;
    public Button deleteButton;

    public void SetSaveName(string sName)
    {
        saveName.text = sName;
    }

    public void SetDays(string ds)
    {
        days.text = ds;
    }

    public void SetCoins(string cns)
    {
        coins.text = cns;
    }
}
