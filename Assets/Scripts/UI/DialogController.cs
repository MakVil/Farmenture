using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        gameObject.SetActive(false);
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }

    public void OpenDialog()
    {
        gameObject.SetActive(true);
    }

    public void SetText(string text, Color color)
    {
        TextMeshProUGUI textElem = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textElem != null)
        {
            textElem.SetText(text);
            textElem.color = color;
        }
        else
        {
            Debug.Log("Dialog text not found!");
        }
    }
}
