using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleEnergyController : MonoBehaviour
{
    public int energyFill = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainCharacterController controller = collision.GetComponent<MainCharacterController>();

        if (controller != null)
        {
            if (controller.Energy < MainCharacterController.MAX_ENERGY)
            {
                controller.ChangeEnergy(energyFill);
                Destroy(gameObject);
            }
        }
    }
}
