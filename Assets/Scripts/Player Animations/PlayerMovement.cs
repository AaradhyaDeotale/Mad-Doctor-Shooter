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
    private float horizontalMove;
    private float verticalMove;
    private bool MoveRight;
    private bool MoveLeft;
    private bool MoveUp;
    private bool MoveDown;
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

    //public MovementJoystick movementJoystick;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveLeft = false;
        MoveRight = false;
        MoveUp = false;
        MoveDown = false;
        // Manually assign AttackButton reference
        //attackButton = FindObjectOfType<AttackButton>();
    }

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
    }

    public void FixedUpdate()
    {
        if (playerDied)
            return;

        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
        HandleShooting();
        CheckIfCanMove();
        //HandleAttackButton();
        HandleInput();
        HorizontalMovement();
        VerticalMovement();// Call the Movement method to handle left and right movement
        rb.velocity = new Vector2(horizontalMove, verticalMove);
    }

    void HandleInput()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

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


    private void HandleAnimation()
    {
        if (!canMove)
            return;

        if (playerAnimation != null)
        {
            if (Mathf.Abs(horizontalMove) > 0 || Mathf.Abs(verticalMove) > 0)
                playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
            else
                playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }

    private void HandleFacingDirection()
    {
        if (playerAnimation != null)
        {
            if (horizontalMove != 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(horizontalMove), transform.localScale.y, transform.localScale.z);

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
        if (!canMove && Time.time > waitBeforeMoving)
        {
            canMove = true;
        }
    }

    public void pointerDownLeft()
    {
        MoveLeft = true;
    }

    public void pointerUpLeft()
    {
        MoveLeft = false;
    }

    public void pointerDownRight()
    {
        MoveRight = true;
    }

    public void pointerUpRight()
    {
        MoveRight = false;
    }
    public void PointerDownUp()
    {
        MoveUp = true;
    }

    public void PointerUpUp()
    {
        MoveUp = false;
    }

    public void PointerDownDown()
    {
        MoveDown = true;
    }

    public void PointerUpDown()
    {
        MoveDown = false;
    }
    void HorizontalMovement()
    {
        if (MoveLeft)
        {
            horizontalMove = -moveSpeed;
        }
        else if (MoveRight)
        {
            horizontalMove = moveSpeed;
        }
        else
        {
            horizontalMove = 0;
        }
    }
    void VerticalMovement()
    {
        if (MoveUp)
        {
            verticalMove = moveSpeed;
        }
        else if (MoveDown)
        {
            verticalMove = -moveSpeed;
        }
        else
        {
            verticalMove = 0;
        }
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