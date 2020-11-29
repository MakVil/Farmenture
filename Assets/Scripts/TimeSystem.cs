using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    private const string PROGRESS_SAVED_TEXT = "Progress saved";

    public GameObject clockTextBox;
    public Image infoBox;

    private const int DAY_START_HOURS = 8;
    private const int DAY_END_HOURS = 24;

    private static int currentHours;
    private static float currentMins;

    public static bool firstInit = true;

    public int day = 1;

    public static TimeSystem Instance;

    private bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if(firstInit)
        { 
            currentHours = DAY_START_HOURS;
            currentMins = 00f;
            firstInit = false;
        }

        UpdateTimeText();

        infoBox.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {
            currentMins += Time.deltaTime * 5;
            if (currentMins >= 60)
            {
                currentHours += 1;
                currentMins = Mathf.Clamp(currentMins - 60, 0, 59);
                if (currentHours >= DAY_END_HOURS)
                {
                    UIController.Instance.EndDay();
                }
            }
            
            if (Mathf.FloorToInt(currentMins % 10) == 0)
                UpdateTimeText();
        }

        if (infoBox.gameObject.activeSelf && infoBox.GetComponent<CanvasRenderer>().GetAlpha() == 0)
        {
            infoBox.gameObject.SetActive(false);
        }
    }

    private void StartDay()
    {
        currentHours = DAY_START_HOURS;
        currentMins = 0;
        day++;

        MainCharacterController mc = MainCharacterController.Instance;

        if (SceneManager.GetActiveScene().name.Equals("FarmScene"))
        {
            mc.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            mc.MoveToStartPosition();
            FarmingController.Instance.AgePlants();

            SaveLoadSystem.Instance.SaveProgress();
        }
        else
        {
            SaveLoadSystem.Instance.SaveTempProgress();
        }
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

    public void UpdateDay(int newDay)
    {
        day = newDay;
        UpdateTimeText();
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

    public void ShowSaveInfo()
    {
        Text infoText = infoBox.GetComponentInChildren<Text>();
        infoText.text = PROGRESS_SAVED_TEXT;

        infoBox.gameObject.SetActive(true);
        infoBox.CrossFadeAlpha(0, 2f, false);
    }
}
