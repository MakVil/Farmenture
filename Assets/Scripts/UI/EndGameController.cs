using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public static EndGameController Instance;
    public GameObject endGameMenu;
    public GameObject thanksBox;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(thanksBox.activeSelf == true && thanksBox.GetComponent<CanvasRenderer>().GetAlpha() == 0)
        {
            TimeSystem.firstInit = true;

            SaveLoadSystem.loadSave = false;
            SaveLoadSystem.loadTemp = false;

            SceneManager.LoadScene(sceneName: "StartScene");
        }
    }

    public void OpenEndGameMenu()
    {
        gameObject.SetActive(true);
        endGameMenu.SetActive(true);
        thanksBox.SetActive(false);
    }

    public void CloseEndGameMenu()
    {
        gameObject.SetActive(false);
    }

    public void YesClicked()
    {
        endGameMenu.SetActive(false);
        thanksBox.SetActive(true);
        thanksBox.GetComponent<Image>().CrossFadeAlpha(0, 10f, false);
    }

    public void NoClicked()
    {
        UIController.Instance.CloseEndGameMenu();
    }
}
