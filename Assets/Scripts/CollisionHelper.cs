using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHelper
{
    public static void EnergyCollision(MainCharacterController controller, int energy)
    {
        EnergyCollision(controller, energy, null);
    }

    public static void EnergyCollision(MainCharacterController controller, int energy, AudioClip sound)
    {
        if (controller != null)
        {
            controller.ChangeEnergy(energy, sound);
        }
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
