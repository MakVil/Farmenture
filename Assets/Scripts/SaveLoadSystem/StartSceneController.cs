using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public GameObject newGameSaveSlotMenu;
    public GameObject loadGameSaveSlotMenu;

    public GameObject confirmDeleteWindow;

    private SaveSlot confirmDeleteSlot;

    public static StartSceneController Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(true);
        newGameSaveSlotMenu.SetActive(false);
        loadGameSaveSlotMenu.SetActive(false);
        confirmDeleteWindow.SetActive(false);
    }

    public void NewGameClicked()
    {
        gameObject.SetActive(false);
        newGameSaveSlotMenu.SetActive(true);
    }

    public void LoadGameClicked()
    {
        gameObject.SetActive(false);
        loadGameSaveSlotMenu.SetActive(true);
    }

    public void BackClicked()
    {
        gameObject.SetActive(true);
        newGameSaveSlotMenu.SetActive(false);
        loadGameSaveSlotMenu.SetActive(false);
    }

    public void DeleteSaveClicked(SaveSlot slot)
    {
        confirmDeleteWindow.SetActive(true);
        confirmDeleteSlot = slot;

        if (LoadGameController.Instance != null)
            LoadGameController.Instance.SetButtonsEnabled(false);
        if (NewGameController.Instance != null)
            NewGameController.Instance.SetButtonsEnabled(false);
    }

    public void ConfirmDeleteClicked()
    {
        DeleteSave();
        confirmDeleteSlot = null;
        confirmDeleteWindow.SetActive(false);
    }

    public void CancelDeleteClicked()
    {
        confirmDeleteSlot = null;
        confirmDeleteWindow.SetActive(false);

        if (LoadGameController.Instance != null)
            LoadGameController.Instance.SetButtonsEnabled(true);
        if (NewGameController.Instance != null)
            NewGameController.Instance.SetButtonsEnabled(true);
    }

    private void DeleteSave()
    {
        SaveLoadSystem.Instance.DeleteSaveSlot(confirmDeleteSlot);

        if (LoadGameController.Instance != null)
            LoadGameController.Instance.SetButtonsEnabled(true);
        if (NewGameController.Instance != null)
            NewGameController.Instance.SetButtonsEnabled(true);

        if (NewGameController.Instance != null)
            NewGameController.Instance.PopulateSaveSlots();
        if (LoadGameController.Instance != null)
            LoadGameController.Instance.PopulateSaveSlots();

    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "FarmScene");
    }

    public void EndGameClicked()
    {
        Application.Quit();
        Debug.Log("Quit application");
    }
}
