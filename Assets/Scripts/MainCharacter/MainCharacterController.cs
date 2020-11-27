using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector3 forestEntryPostion;

    public Vector3 startPosition;
    public float movementSpeed = 4f;
    private Vector2 movement;
    private bool faceRight;
    public bool FaceRight { get => faceRight; }
    private bool faceUp;
    private bool idleHor;

    public bool canMove;
    public bool inCutscene;

    public const int MAX_ENERGY = 100;
    private int currentEnergy;

    public int Energy { get => currentEnergy; set{ currentEnergy = value; UpdateEnergyBar(); }}

    private int money = 10;
    public int Money { get => money; set { money = value; } }

    private float timeInvincible = 1.0f;
    private float timeLeftInvincible;
    private bool isInvincible;
    
    public GameObject swordPrefab;

    private AudioSource audioSource;

    private Animator animator;
    public Animator Animator { get => animator; }

    public static MainCharacterController Instance { get; private set; }

    public Sprite charSprite;

    public AudioClip footStepSound;
    public AudioClip deadSound;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        startPosition = gameObject.transform.position;
        charSprite = gameObject.GetComponent<Sprite>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        
        Energy = MAX_ENERGY;

        timeLeftInvincible = 0.0f;
        isInvincible = false;

        faceRight = false;
        canMove = true;

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!inCutscene)
        {
            if (canMove)
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
                else if (Input.GetButtonDown("Interact"))
                {
                    Interact();
                }
            }

            if (Input.GetButtonDown("Inventory"))
            {
                OpenCloseInventory();
            }

            if (isInvincible)
            {
                timeLeftInvincible = Mathf.Clamp(timeLeftInvincible - Time.deltaTime, 0, timeInvincible);
                if (Mathf.Approximately(timeLeftInvincible, 0))
                {
                    isInvincible = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (!inCutscene)
        {
            Vector2 position = rb.position;
            if (canMove)
            {
                position.x = position.x + GetMovementSpeed() * movement.x;
                position.y = position.y + GetMovementSpeed() * movement.y;
            }

            rb.MovePosition(position);
        }
    }

    private void OpenCloseInventory()
    {
        if(MainCharInventory.Instance.gameObject.activeSelf)
        {
            UIController.Instance.CloseInventory();
        }
        else if(WebStoreController.Instance != null && WebStoreController.Instance.gameObject.activeSelf)
        {
            UIController.Instance.CloseWebStore();
        }
        else
        {
            UIController.Instance.OpenInventory();
        }
    }

    private void Interact()
    {
        if (UIController.Instance.IsAllClosed())
        {
            RaycastHit2D hit;
            if (idleHor)
                hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceRight ? Vector2.right : Vector2.left), 1f, LayerMask.GetMask("Interact"));
            else
                hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceUp ? Vector2.up : Vector2.down), 1f, LayerMask.GetMask("Interact"));

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Computer"))
                {
                    UIController.Instance.OpenWebStore();
                }
                else if (hit.collider.GetComponent<NPCController>() != null)
                {
                    hit.collider.GetComponent<NPCController>().StartDialog();
                }
                else if (hit.collider.CompareTag("Bed"))
                {
                    UIController.Instance.EndDay();
                }
            }
            else if (HotbarController.Instance.IsSeedSelected())
            {
                RaycastHit2D hitDirt;
                if (idleHor)
                    hitDirt = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceRight ? Vector2.right : Vector2.left), 0.5f, LayerMask.GetMask("DirtPlot"));
                else
                    hitDirt = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, (faceUp ? Vector2.up : Vector2.down), 0.5f, LayerMask.GetMask("DirtPlot"));

                if (hitDirt.collider != null && hitDirt.collider.GetComponent<DirtPlot>() != null)
                    hitDirt.collider.GetComponent<DirtPlot>().PlantSeed(HotbarController.Instance.GetSelectedSeed());
            }
            else
            {
                HotbarController.Instance.Action();
            }
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
                if (hitSound != null)
                    PlaySound(hitSound);
            }

            Energy = Mathf.Clamp(Energy + amount, -1, MAX_ENERGY);

            if(Energy <= 0)
            {
                Die();
            }

            UpdateEnergyBar();
        }
    }

    public void SetInvincible()
    {
        isInvincible = true;
        timeLeftInvincible = timeInvincible;
    }

    public void RefillEnergy()
    {
        Energy = MAX_ENERGY;
        UpdateEnergyBar();
    }

    private void UpdateEnergyBar()
    {
        if (EnergyBarController.instance != null && MAX_ENERGY != 0)
        {
            EnergyBarController.instance.SetEnergyValue(currentEnergy / (float) MAX_ENERGY);
        }
    }

    public void Die()
    {
        canMove = false;
        animator.SetTrigger("Die");
        PlaySound(deadSound);
    }

    public void AfterDeath()
    {
        NightController.Instance.StartNight();
    }

    private void SwingSword()
    {
        if (canMove)
        {
            GameObject swordObject = Instantiate(swordPrefab, rb.position + GetSwordPositionOffset(), Quaternion.identity);
            swordObject.GetComponent<SpriteRenderer>().sortingOrder = (faceUp ? 0 : 1);
            SwordController sword = swordObject.GetComponent<SwordController>();
            if (sword != null)
            {
                if(idleHor)
                    sword.Swing(faceRight ? 1f : -1f, 0);
                else
                    sword.Swing(0, faceUp ? 1f : -1f);
                canMove = false;
            }
        }
    }

    public void MoveToStartPosition()
    {
        gameObject.transform.position = startPosition;
    }

    public void MoveTo(float x, float y)
    {
        gameObject.transform.position = new Vector3(x, y);
    }

    public void MoveToForestEntry()
    {
        gameObject.transform.position = forestEntryPostion;
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

    public Vector2 GetSwordPositionOffset()
    {
        Vector2 offset;
        if (!idleHor)
            offset = Vector2.up * (faceUp ?  0.7f :  0.2f);
        else
            offset = Vector2.up * 0.4f + (faceRight ? Vector2.right : Vector2.left) * 0.1f;

        return offset;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIController.Instance.UpdateMoney();
    }

    public void ReduceMoney(int amount)
    {
        money = Mathf.Clamp(money - amount, 0, 99999999);
        UIController.Instance.UpdateMoney();
    }

    public void PlayFootStep()
    {
        PlaySound(footStepSound);
    }
}
