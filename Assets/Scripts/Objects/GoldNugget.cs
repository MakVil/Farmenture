using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldNugget : PickUpItem
{
    public AudioClip collectSound;

    public void PlayCollectSound()
    {
        MainCharacterController.Instance.PlaySound(collectSound);
    }
}
