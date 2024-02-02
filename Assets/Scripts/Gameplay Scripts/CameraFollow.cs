using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float SmoothSpeed = 2f;

    [SerializeField]
    private float playerBoundMin_Y = -1f, playerBoundMin_X = -58f, playerBoundMax_X = 60f;

    [SerializeField]
    private float Y_Gap = -0.9f;

    private Vector3 tempPos;

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void Update()
    {

        if (!playerTarget)
            return;

        tempPos = transform.position;

        if (playerTarget.position.y <= playerBoundMin_Y)
        {
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y, -10f), Time.deltaTime * SmoothSpeed);

        }
        else
        {
            tempPos = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y + Y_Gap, -10f), Time.deltaTime * SmoothSpeed);

        }

        if(tempPos.x > playerBoundMax_X)
            tempPos.x = playerBoundMax_X;

        if (tempPos.x < playerBoundMin_X)
            tempPos.x = playerBoundMin_X;

        transform.position = tempPos;
    }
}//class
