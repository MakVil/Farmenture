using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHelper
{
    public static void EnergyCollision(int energy)
    {
        EnergyCollision(energy, null);
    }

    public static void EnergyCollision(int energy, AudioClip sound)
    {
        MainCharacterController.Instance.ChangeEnergy(energy, sound);
    }
    public static void HealthCollision(GameObject obj, int health)
    {
        if(obj.GetComponent<CrowController>() != null)
        {
            CrowController cont = obj.GetComponent<CrowController>();
            cont.ChangeHealth(health);
        }
    }
}
