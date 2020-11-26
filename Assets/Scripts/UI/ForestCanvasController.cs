using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestCanvasController : MonoBehaviour
{
    public static ForestCanvasController Instance;
    private static System.Random rnd;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        gameObject.SetActive(false);

        rnd = new System.Random();
    }

    public void OpenForestEntryMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseForestEntryMenu()
    {
        gameObject.SetActive(false);
    }

    public void NoClicked()
    {
        UIController.Instance.CloseForestEntryMenu();
    }

    public void YesClicked()
    {
        SaveLoadSystem.loadSave = false;
        SaveLoadSystem.loadTemp = true;

        SaveLoadSystem.Instance.SaveTempDirtProgress();
        SaveLoadSystem.Instance.SaveTempProgress();

        int forest = rnd.Next(1, 4);
        SceneManager.LoadScene(sceneName: "ForestScene" + forest);
    }

    public void BackToHome()
    {
        SaveLoadSystem.loadSave = false;
        SaveLoadSystem.loadTemp = true;

        SaveLoadSystem.Instance.SaveTempProgress();

        SceneManager.LoadScene(sceneName: "FarmScene");
    }
}
