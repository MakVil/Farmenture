using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance;
    public GameObject settingsMenu;
    public GameObject helpMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        gameObject.SetActive(true);
        settingsMenu.SetActive(true);
        helpMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
        settingsMenu.SetActive(false);
        helpMenu.SetActive(false);
    }

    public void BackClicked()
    {
        UIController.Instance.CloseSettings();
    }

    public void EndGameToMainMenuClicked()
    {
        TimeSystem.firstInit = true;

        SaveLoadSystem.loadSave = false;
        SaveLoadSystem.loadTemp = false;

        SceneManager.LoadScene(sceneName: "StartScene");
    }

    public void EndGameToDesktopClicked()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void HelpMenuClicked()
    {
        settingsMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void BackHelpMenuClicked()
    {
        settingsMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
}
