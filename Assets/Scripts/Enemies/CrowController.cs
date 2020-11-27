using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    private const int MAX_HEALTH = 20;

    public GameObject hitEffectPrefab;
    public GameObject dropItemPrefab;

    private Rigidbody2D rb;
    public float speed = 3f;

    private Vector2 oldPosition;
    public Vector2 maxMovement;
    private Vector2 maxPosition;
    private Vector2 minPosition;
    private bool goRight;
    private bool goUp;

    private int currentHealth;

    private int energyDamage = -10;

    private bool isDead;

    public AudioClip hitSound;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 position = rb.position;
        maxPosition.x = position.x + maxMovement.x;
        minPosition.x = position.x - maxMovement.x;
        maxPosition.y = position.y + maxMovement.y;
        minPosition.y = position.y - maxMovement.y;
        goRight = true;
        goUp = true;

        currentHealth = MAX_HEALTH;
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Vector2 position = rb.position;
            position.x = Mathf.Clamp(position.x + Time.deltaTime * speed * (goRight ? 1 : -1), minPosition.x, maxPosition.x);
            position.y = Mathf.Clamp(position.y + Time.deltaTime * speed * (goUp ? 1 : -1), minPosition.y, maxPosition.y);

            rb.MovePosition(position);

            if (Mathf.Approximately(oldPosition.x, position.x))
                goRight = !goRight;
            else if (position.x <= minPosition.x)
                goRight = true;
            else if (position.x >= maxPosition.x)
                goRight = false;

            if (Mathf.Approximately(oldPosition.y, position.y))
                goUp = !goUp;
            else if (position.y <= minPosition.y)
                goUp = true;
            else if (position.y >= maxPosition.y)
                goUp = false;

            oldPosition = position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MainCharacterController>() != null)
        {
            CollisionHelper.EnergyCollision(energyDamage, hitSound);
            MainCharacterController.Instance.SetInvincible();
        }
    }

    public void ChangeHealth(int amount)
    {
        Instantiate(hitEffectPrefab, rb.position, Quaternion.identity);

        currentHealth += amount;
        if (currentHealth <= 0)
        {
            isDead = true;
            gameObject.GetComponent<Animator>().SetTrigger("Die");
        }

    }

    public void AfterDeath()
    {
        Instantiate(dropItemPrefab, rb.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
