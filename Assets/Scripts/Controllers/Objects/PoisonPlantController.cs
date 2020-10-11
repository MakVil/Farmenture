using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlantController : MonoBehaviour
{
    private int energyDamage = -5;
    public AudioClip hitSound;

    private void OnTriggerStay2D(Collider2D collision)
    {
        CollisionHelper.EnergyCollision(collision.GetComponent<MainCharacterController>(), energyDamage, hitSound);
    }
}
