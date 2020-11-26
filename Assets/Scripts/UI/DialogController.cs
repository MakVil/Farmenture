using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(gameObject.activeSelf && gameObject.GetComponent<CanvasRenderer>().GetAlpha() == 0)
        {
            UIController.Instance.CloseDialog();
        }
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

    public void FadeDialog()
    {
        gameObject.GetComponent<Image>().CrossFadeAlpha(0f, 4f, false);
    }
}
