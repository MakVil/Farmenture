﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Rigidbody2D rb;
    private int healthDamage = -5;
    private Animator animator;
    private GameObject mainCharacter;
    private bool hasDamaged;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hasDamaged = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(MainCharacterController.Instance.GetComponent<Rigidbody2D>().position + MainCharacterController.Instance.getSwordPositionOffset());
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (!hasDamaged && collision.CompareTag("Enemy"))
        {
            CollisionHelper.HealthCollision(collision.gameObject, healthDamage);
            hasDamaged = true;
        }
    }

    public void Swing(float faceRight, GameObject mc)
    {
        if (animator != null)
        {
            mainCharacter = mc;
            animator.SetFloat("MoveX", faceRight);
            animator.SetTrigger("Swing");
        }
        else
            Debug.Log("Animator not found");
    }

    public void AfterSwing()
    {
        Destroy(gameObject);
        MainCharacterController.Instance.canMove = true;
    }
}
