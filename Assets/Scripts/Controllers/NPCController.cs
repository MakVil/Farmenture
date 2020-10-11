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
        GameObject dialog = getDialog();
        if (dialog != null)
        {
            dialog.SetActive(false);
        }
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
                GameObject dialog = getDialog();
                if (dialog != null)
                {
                    dialog.SetActive(false);
                }
            }
        }

    }

    public void StartDialog()
    {
        GameObject dialog = getDialog();
        if (dialog != null)
        {
            dialog.SetActive(true);
            timerDisplay = displayTime;
        }

    }

    private GameObject getDialog()
    {
        GameObject dialog = null;

        DialogController dialogCont = DialogController.instance;
        if (dialogCont != null && dialogCont.gameObject != null)
        {
            dialog = dialogCont.gameObject;
        }

        return dialog;
    }
}
