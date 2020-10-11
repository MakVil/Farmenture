using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
