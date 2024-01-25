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
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAnimation();
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
        // Check if playerAnimation is not null before calling PlayAnimation
        if (playerAnimation != null)
        {
            if (Mathf.Abs(xAxis) > 0 || Mathf.Abs(yAxis) > 0)
                playerAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
            else
                playerAnimation.PlayAnimation(TagManager.IDLE_ANIMATION_NAME);
        }
    }
} //class
