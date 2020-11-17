using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public GameObject clockText;

    private const int DAY_START_HOURS = 8;
    private const int DAY_END_HOURS = 9;

    private int currentHours;
    private float currentMins;

    public GameObject mainChar;
    
    void Start()
    {
        currentHours = DAY_START_HOURS;
        currentMins = 50;

        UpdateTimeText();
    }
    
    void FixedUpdate()
    {
        currentMins += Time.deltaTime;
        if (currentMins >= 60)
        {
            currentHours += 1;
            currentMins = Mathf.Clamp(currentMins - 60, 0, 59);
            if (currentHours >= DAY_END_HOURS)
            {
                StartDay();
            }
        }
        UpdateTimeText();
    }

    private void StartDay()
    {
        currentHours = DAY_START_HOURS;
        currentMins = 0;

        MainCharacterController mc = mainChar.GetComponent<MainCharacterController>();
        if (mc != null)
        {
            mc.moveToStartPosition();
            mc.RefillEnergy();
        }

        FarmingController.Instance.AgePlants();

        SaveLoadSystem.Instance.SaveProgress();
    }

    private void UpdateTimeText()
    {
        TextMeshProUGUI text = clockText.GetComponent<TextMeshProUGUI>();
        if (text != null)
        {
            StringBuilder timeText = new StringBuilder();
            timeText.Append(currentHours < 10 ? "0" + currentHours.ToString() : currentHours.ToString());
            timeText.Append(":");
            timeText.Append(currentMins < 10 ? "0" + Mathf.FloorToInt(currentMins).ToString() : Mathf.FloorToInt(currentMins).ToString());

            text.SetText(timeText.ToString());
        }
    }
}
