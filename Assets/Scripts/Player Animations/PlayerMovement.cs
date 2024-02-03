using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float minBound_X = -67f, maxBound_X = 70f, minBound_Y = -3.3f, maxBound_Y = 0f;

    private Vector3 tempPos;
    private float xAxis, yAxis;

    public PlayerAnimation playerAnimation;
    //public AttackButton attackButton; // Add reference to AttackButton script

    [SerializeField]
    private float shootWaitTime = 0.5f;

    private float waitBeforeShooting;

    [SerializeField]
    private float moveWaitTime = 0.3f;
    private float waitBeforeMoving;

    private bool canMove = true;
    private PlayerShootingManager playerShootingManager;

    private bool playerDied;

    public MovementJoystick joystick;

    void Start()
    {
        // Manually assign AttackButton reference
        //attackButton = FindObjectOfType<AttackButton>();
    }

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
    }

    public void Update()
    {
        if (playerDied)
            return;

        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
        HandleShooting();
        //HandleAttackButton();
        HandleInput();
         Vector2 direction = joystick.GetJoystickInput();

        // Move the player based on joystick input
        transform.Translate(new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime);
    }

    void HandleInput()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

        Vector2 direction = joystick.GetJoystickInput();
        transform.Translate(new Vector3(direction.x, direction.y, 0f) * moveSpeed * Time.deltaTime);

    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);
        if (!canMove)
            return;

        tempPos = transform.position;
        tempPos.x += xAxis * moveSpeed * Time.deltaTime;
        tempPos.y += yAxis * moveSpeed * Time.deltaTime;

        if (tempPos.x < minBound_X)
            tempPos.x = minBound_X;

        if (tempPos.x > maxBound_X)
            tempPos.x = maxBound_X;

        if (tempPos.y < minBound_Y)
            tempPos.y = minBound_Y;

        if (tempPos.y > maxBound_Y)
            tempPos.y = maxBound_Y;

        transform.position = tempPos;
    }

   /* void HandleAttackButton()
    {
        // Check if the attack button is pressed
        if (attackButton.IsAttacking())
        {
            playerAnimation.PlayAttackAnimation();
            Shoot();  // Call the shooting method when the attack button is pressed
        }
    } */


    void HandleAnimation()
    {
        if (!canMove)
            return;

        if (playerAnimation != null)
        {
            if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
                playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
            else
                playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }

    void HandleFacingDirection()
    {
        if (playerAnimation != null)
        {
            if (xAxis != 0) // Check if the player is moving horizontally
            {
                if (xAxis > 0)
                    playerAnimation.SetFacingDirection(true); // Face right when moving right
                else
                    playerAnimation.SetFacingDirection(false); // Face left when moving left
            }

            // Ensure Y and Z scales are not set to 0
            if (Mathf.Abs(transform.localScale.y) < 0.001f)
                transform.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);

            if (Mathf.Abs(transform.localScale.z) < 0.001f)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1f);
        }
    }

    void StopMovement()
    {
        canMove = false;
        waitBeforeMoving = Time.time + moveWaitTime;
    }

    public void Shoot()
    {
        waitBeforeShooting = Time.time + shootWaitTime;
        StopMovement();
        playerAnimation.PlayAnimation(TagManager.ATTACK_ANIMATION_NAME);

        playerShootingManager.Shoot(transform.localScale.x);
    }

    void CheckIfCanMove()
    {
        if (Time.time > waitBeforeMoving)
            canMove = true;
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Shooting key pressed");
            if (Time.time > waitBeforeShooting)
                Shoot();
        }
    }

    public void PlayerDied()
    {
        playerDied = true;
        playerAnimation.PlayAnimation(TagManager.DEATH_ANIMATION_NAME);
        Invoke("DestroyPlayerAfterDelay", 2f);
    }

    void DestroyPlayerAfterDelay()
    {
        Destroy(gameObject);
    }
}//class
