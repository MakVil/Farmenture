using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    private float displayTime = 4.0f;
    private float timerDisplay;

    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.CloseDialog();
        timerDisplay = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                UIController.Instance.CloseDialog();
            }
        }

    }

    public void StartDialog()
    {
        UIController.Instance.OpenDialog();
        timerDisplay = displayTime;
    }
}
