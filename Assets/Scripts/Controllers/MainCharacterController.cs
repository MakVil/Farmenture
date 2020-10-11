using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    public float movementSpeed = 5f;
    private Vector2 movement;
    private bool faceRight;
    public bool FaceRight { get => faceRight; }

    public bool isSwinging;

    public int maxEnergy = 100;
    private int currentEnergy;

    public int Energy { get => currentEnergy; private set => currentEnergy = value; }

    private float timeInvincible = 2.0f;
    private float timeLeftInvincible;
    private bool isInvincible;

    public GameObject swordPrefab;

    private AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();

        Energy = maxEnergy;

        timeLeftInvincible = 0.0f;
        isInvincible = false;

        faceRight = false;
        isSwinging = false;

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x > 0)
            faceRight = true;
        else if (movement.x < 0)
            faceRight = false;

        if (isInvincible)
        {
            timeLeftInvincible = Mathf.Clamp(timeLeftInvincible - Time.deltaTime, 0, timeInvincible);
            if (Mathf.Approximately(timeLeftInvincible, 0))
            {
                isInvincible = false;
            }
        }

        if (Input.GetButtonDown("Attack"))
        {
            SwingSword();
        }
        else if(Input.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void Interact()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceRight ? Vector2.right : Vector2.left), 1f, LayerMask.GetMask("NPC"));

        if (hit.collider != null && hit.collider.GetComponent<NPCController>() != null)
        {
            hit.collider.GetComponent<NPCController>().StartDialog();
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        if (!isSwinging)
        {
            position.x = position.x + GetMovementSpeed() * movement.x;
            position.y = position.y + GetMovementSpeed() * movement.y;
        }

        rb.MovePosition(position);
    }

    public void ChangeEnergy(int amount)
    {
        ChangeEnergy(amount, null);
    }

    public void ChangeEnergy(int amount, AudioClip hitSound)
    {
        if (amount >= 0 || !isInvincible)
        {
            if (amount < 0)
            {
                isInvincible = true;
                timeLeftInvincible = timeInvincible;
                if (hitSound != null)
                    PlaySound(hitSound);
            }

            Energy = Mathf.Clamp(Energy + amount, -1, maxEnergy);

            if(EnergyBarController.instance != null && maxEnergy != 0)
            {
                EnergyBarController.instance.SetEnergyValue(currentEnergy / (float) maxEnergy);
            }
        }
    }

    private void SwingSword()
    {
        if (!isSwinging)
        {
            GameObject swordObject = Instantiate(swordPrefab, rb.position + getSwordPositionOffset(), Quaternion.identity);
            SwordController sword = swordObject.GetComponent<SwordController>();
            if (sword != null)
            {
                sword.Swing(faceRight ? 1.0f : 0.0f, gameObject);
                isSwinging = true;
            }
        }
    }

    public void PlaySound(AudioClip sound)
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }

    private float GetMovementSpeed()
    {
        return movementSpeed * Time.deltaTime;
    }

    public Vector2 getSwordPositionOffset()
    {
        return Vector2.up * 0.4f + (faceRight ? Vector2.right : Vector2.left) * 0.3f;
    }
}
