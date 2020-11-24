using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void CloseDialog()
    {
        gameObject.SetActive(false);
    }

    public void OpenDialog()
    {
        gameObject.SetActive(true);
    }
}
