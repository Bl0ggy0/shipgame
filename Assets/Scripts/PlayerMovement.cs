using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    [Header("Dodge")]
    public float dodgeSpeed = 12f;
    public float dodgeDuration = 0.2f;
    public float dodgeCooldown = 0.5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private bool isDodging;
    private bool canDodge = true;
    private float dodgeTimer;
    private float cooldownTimer;

    // Other scripts can check this to see if the player has iframes
    public bool IsInvincible => isDodging;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y += 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y -= 1;

        if (Keyboard.current.aKey.isPressed)
            movement.x -= 1;

        if (Keyboard.current.dKey.isPressed)
            movement.x += 1;

        movement = movement.normalized;

        // Dodge
        if (Keyboard.current.spaceKey.wasPressedThisFrame && canDodge && movement != Vector2.zero)
        {
            isDodging = true;
            canDodge = false;
            dodgeTimer = dodgeDuration;
        }

        // Dodge timer
        if (isDodging)
        {
            dodgeTimer -= Time.deltaTime;

            if (dodgeTimer <= 0)
            {
                isDodging = false;
            }
        }

        // Cooldown timer
        if (!canDodge)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= dodgeCooldown)
            {
                canDodge = true;
                cooldownTimer = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDodging)
        {
            // Dash in the direction you're currently moving
            rb.MovePosition(rb.position + movement * dodgeSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}