using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    public static SettingsController Instance;

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
    }

    public void CloseSettings()
    {
        gameObject.SetActive(false);
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
}
