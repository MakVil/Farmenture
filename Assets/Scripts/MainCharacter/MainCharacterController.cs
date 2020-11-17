using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector3 startPosition;
    public float movementSpeed = 5f;
    private Vector2 movement;
    private bool faceRight;
    public bool FaceRight { get => faceRight; }
    private bool faceUp;
    private bool idleHor;

    public bool isSwinging;

    public int maxEnergy = 100;
    private int currentEnergy;

    public int Energy { get => currentEnergy; set{ currentEnergy = value; UpdateEnergyBar(); }}

    private float timeInvincible = 2.0f;
    private float timeLeftInvincible;
    private bool isInvincible;

    public GameObject swordPrefab;

    private AudioSource audioSource;

    private Animator animator;

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
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.x > 0)
        {
            faceRight = true;
            idleHor = true;
        }
        else if (movement.x < 0)
        {
            faceRight = false;
            idleHor = true;
        }

        if (movement.y > 0)
        {
            faceUp = true;
            idleHor = false;
        }
        else if (movement.y < 0)
        {
            faceUp = false;
            idleHor = false;
        }

        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetTrigger("Idle");

            if (idleHor)
            {
                animator.SetFloat("faceRight", faceRight ? 1.0f : -1.0f);
                animator.SetFloat("faceUp", 0f);
            }
            else
            {
                animator.SetFloat("faceRight", 0f);
                animator.SetFloat("faceUp", faceUp ? 1.0f : -1.0f);
            }
        }
        else
        {
            animator.SetTrigger("Move");

            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }

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

        if (Input.GetButtonDown("Inventory"))
        {
            OpenCloseInventory();
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

    private void OpenCloseInventory()
    {
        MainCharInventory.Instance.gameObject.SetActive(!MainCharInventory.Instance.gameObject.activeSelf);
        HotbarController.Instance.gameObject.SetActive(!HotbarController.Instance.gameObject.activeSelf);
    }

    private void Interact()
    {

        RaycastHit2D hit;
        if(idleHor)
            hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceRight ? Vector2.right : Vector2.left), 1f, LayerMask.GetMask("NPC"));
        else
            hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceUp ? Vector2.up : Vector2.down), 1f, LayerMask.GetMask("NPC"));

        if (hit.collider != null && hit.collider.GetComponent<NPCController>() != null)
        {
            hit.collider.GetComponent<NPCController>().StartDialog();
        }
        else if (HotbarController.Instance.IsSeedSelected())
        {
            Debug.Log("This is seed!");

            RaycastHit2D hitDirt;
            if (idleHor)
                hitDirt = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceRight ? Vector2.right : Vector2.left), 1f, LayerMask.GetMask("DirtPlot"));
            else
                hitDirt = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceUp ? Vector2.up : Vector2.down), 1f, LayerMask.GetMask("DirtPlot"));

            if (hitDirt.collider != null && hitDirt.collider.GetComponent<DirtPlot>() != null)
                hitDirt.collider.GetComponent<DirtPlot>().PlantSeed(HotbarController.Instance.GetSelectedSeed());
        }
        else
        {
            HotbarController.Instance.Action();
        }
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

            UpdateEnergyBar();
        }
    }

    public void refillEnergy()
    {
        Energy = maxEnergy;
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        if (EnergyBarController.instance != null && maxEnergy != 0)
        {
            EnergyBarController.instance.SetEnergyValue(currentEnergy / (float)maxEnergy);
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

    public void moveToStartPosition()
    {
        gameObject.transform.position = startPosition;
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
        Vector2 offset = Vector2.up * 0.4f;
        if(!idleHor)
            offset += (faceRight ? Vector2.right : Vector2.left) * 0.3f;
        return offset;
    }
}
