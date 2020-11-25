using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestOutroController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacterController controller = collision.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            ForestCanvasController.Instance.BackToHome();
        }
    }
}
