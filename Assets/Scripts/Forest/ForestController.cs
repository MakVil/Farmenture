using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestController : MonoBehaviour
{
    // Common
    [SerializeField]
    private float startX;
    public float StartX { get => startX; }
    [SerializeField]
    private float startY;
    public float StartY { get => startY; }

    // Crow related variables
    [SerializeField]
    private float minX;
    public float MinX { get => minX; }
    [SerializeField]
    private float maxX;
    public float MaxX { get => maxX; }
    [SerializeField]
    private float minY;
    public float MinY { get => minY; }
    [SerializeField]
    private float maxY;
    public float MaxY { get => maxY; }

    [SerializeField]
    private int nOfCrows;

    public GameObject crowPrefab;

    // Energy item related variables
    [SerializeField]
    private int nOfEnergyItems;

    public GameObject energyItemPrefab;

    // Poison plant related variables
    [SerializeField]
    private int nOfPoisonPlants;

    public GameObject poisonPlantPrefab;

    private void Start()
    {
        // Generate the crows
        for (int i = 0; i < nOfCrows; i++)
        {
            float posX = Random.Range(MinX, MaxX);
            float posY = Random.Range(MinY, MaxY);
            float moveX = Random.Range(0f, 7f);
            float moveY = Random.Range(0f, 7f);

            GameObject crow = Instantiate(crowPrefab, new Vector3(posX, posY), Quaternion.identity);
            CrowController controller = crow.GetComponent<CrowController>();
            if (controller != null)
            {
                controller.maxMovement = new Vector2(moveX, moveY);
            }
            else
            {
                Debug.Log("Crow controller not found!");
            }
        }

        // Generate the energy items
        for (int i = 0; i < nOfEnergyItems; i++)
        {
            float posX = Random.Range(MinX, MaxX);
            float posY = Random.Range(MinY, MaxY);

            Instantiate(energyItemPrefab, new Vector3(posX, posY), Quaternion.identity);

        }

        // Generate the poison plants
        for (int i = 0; i < nOfPoisonPlants; i++)
        {
            float posX = Random.Range(MinX, MaxX);
            float posY = Random.Range(MinY, MaxY);

            Instantiate(poisonPlantPrefab, new Vector3(posX, posY), Quaternion.identity);

        }

        MainCharacterController.Instance.MoveTo(StartX, StartY);
    }
}
