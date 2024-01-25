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
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleFacingDirection();
    }

    void HandleMovement()
    {
        xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        yAxis = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);
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

    void HandleAnimation()
    {
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


}
