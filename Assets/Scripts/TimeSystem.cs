using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public GameObject clockTextBox;

    private const int DAY_START_HOURS = 8;
    private const int DAY_END_HOURS = 9;

    private int currentHours;
    private float currentMins;

    public int day = 1;

    public static TimeSystem Instance;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentHours = DAY_START_HOURS;
        currentMins = 00f;

        UpdateTimeText();
    }
    
    void FixedUpdate()
    {
        if (!isPaused)
        {
            currentMins += Time.deltaTime;
            if (currentMins >= 60)
            {
                currentHours += 1;
                currentMins = Mathf.Clamp(currentMins - 60, 0, 59);
                if (currentHours >= DAY_END_HOURS)
                {
                    UIController.Instance.EndDay();
                }
            }
            UpdateTimeText();
        }
    }

    private void StartDay()
    {
        currentHours = DAY_START_HOURS;
        currentMins = 0;
        day++;

        MainCharacterController mc = MainCharacterController.Instance;
        if (mc != null)
        {
            mc.MoveToStartPosition();
            mc.RefillEnergy();
        }

        FarmingController.Instance.AgePlants();

        SaveLoadSystem.Instance.SaveProgress();
    }

    public void GoToSleep()
    {
        StartDay();
    }

    private void UpdateTimeText()
    {
        TextMeshProUGUI text = clockTextBox.GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            StringBuilder timeText = new StringBuilder();
            timeText.Append("Day ");
            timeText.Append(day);
            timeText.Append("  ");
            timeText.Append(currentHours < 10 ? "0" + currentHours.ToString() : currentHours.ToString());
            timeText.Append(":");
            timeText.Append(currentMins < 10 ? "0" + Mathf.FloorToInt(currentMins).ToString() : Mathf.FloorToInt(currentMins).ToString());

            text.SetText(timeText.ToString());
        }
    }

    public void HideTimeBox()
    {
        clockTextBox.SetActive(false);
    }

    public void ShowTimeBox()
    {
        clockTextBox.SetActive(true);
    }

    public void SetPaused(bool val)
    {
        isPaused = val;
    }
}
