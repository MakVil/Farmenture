using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlantController : MonoBehaviour
{
    private int energyDamage = -5;
    public AudioClip hitSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<MainCharacterController>() != null)
        {
            CollisionHelper.EnergyCollision(energyDamage, hitSound);
            MainCharacterController.Instance.SetInvincible();
        }
    }
}
