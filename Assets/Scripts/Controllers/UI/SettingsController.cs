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

    public void EndGameClicked()
    {
        SceneManager.LoadScene(sceneName: "StartScene");
    }
}
