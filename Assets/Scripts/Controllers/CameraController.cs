using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private GameObject mainCharacter;

    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = MainCharacterController.Instance.gameObject;
        offset = transform.position - mainCharacter.transform.position;
        offset.x = 0;
        offset.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCharacter.transform.position + offset;
    }
}
