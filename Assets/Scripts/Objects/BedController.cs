using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public static BedController Instance;

    public Animator animator;

    public Sprite sleepSprite;

    private void Awake()
    {
        Instance = this;
        animator = gameObject.GetComponent<Animator>();
    }

    public void GoToBed()
    {
        animator.SetTrigger("GoToBed");
    }

    public void WakeUp()
    {
        MainCharacterController.Instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        SetToSleepSprite();
        animator.SetTrigger("WakeUp");
    }

    public void HideMainChar()
    {
        MainCharacterController.Instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ShowMainChar()
    {
        MainCharacterController.Instance.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        MainCharacterController.Instance.canMove = true;
    }

    public void SetToSleepSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sleepSprite;
    }
}
