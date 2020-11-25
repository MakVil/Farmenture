using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightController : MonoBehaviour
{
    public static NightController Instance;
    private TextMeshProUGUI textMesh;
    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        animator = gameObject.GetComponentInChildren<Animator>();
        gameObject.SetActive(false);
    }

    private void Start()
    {
        int nextDay = TimeSystem.Instance.day;
        if (textMesh != null)
            textMesh.text = "Day " + nextDay;
        else
            Debug.Log("Text field not found!");
    }

    public void StartNight()
    {
        TimeSystem.Instance.SetPaused(true);

        MainCharacterController.Instance.canMove = false;
        MainCharacterController.Instance.Animator.SetTrigger("Idle");

        if(SceneManager.GetActiveScene().name.Equals("FarmScene"))
            BedController.Instance.GoToBed();

        gameObject.SetActive(true);

        int nextDay = TimeSystem.Instance.day + 1;
        if (textMesh != null)
            textMesh.text = "Day " + nextDay;
        else
            Debug.Log("Text field not found!");

        gameObject.SetActive(true);
        animator.SetTrigger("Night");


        TimeSystem.Instance.GoToSleep();
    }

    public void MoveToFarm()
    {
        if (!SceneManager.GetActiveScene().name.Equals("FarmScene"))
        {
            SaveLoadSystem.loadSave = true;
            SaveLoadSystem.loadTemp = false;
            SaveLoadSystem.showSaveInfo = true;

            SceneManager.LoadScene(sceneName: "FarmScene");
        }
    }

    public void EndNightAfterForest()
    {
        MainCharacterController.Instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        int nextDay = TimeSystem.Instance.day;
        if (textMesh != null)
            textMesh.text = "Day " + nextDay;
        else
            Debug.Log("Text field not found!");

        gameObject.SetActive(true);
        animator.SetTrigger("EndNight");
    }

    public void WakeUp()
    {
        BedController.Instance.WakeUp();
    }

    public void EndNight()
    {
        gameObject.SetActive(false);

        TimeSystem.Instance.ShowSaveInfo();
        TimeSystem.Instance.SetPaused(false);
    }
}
