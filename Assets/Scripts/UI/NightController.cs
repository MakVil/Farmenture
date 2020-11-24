using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NightController : MonoBehaviour
{
    public static NightController Instance;
    private TextMeshProUGUI textMesh;
    private Animator animator;

    private void Awake()
    {
        Instance = this;
        textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        animator = gameObject.GetComponentInChildren<Animator>();
        gameObject.SetActive(false);
    }

    public void StartNight()
    {
        MainCharacterController.Instance.canMove = false;
        gameObject.SetActive(true);

        int nextDay = TimeSystem.Instance.day + 1;
        if (textMesh != null)
            textMesh.text = "Day " + nextDay;
        else
            Debug.Log("Text field not found!");

        animator.SetTrigger("Night");
    }

    public void DuringNight()
    {
        TimeSystem.Instance.GoToSleep();

        animator.SetTrigger("Day");
    }

    public void EndNight()
    {
        gameObject.SetActive(false);
        MainCharacterController.Instance.canMove = true;
    }
}
