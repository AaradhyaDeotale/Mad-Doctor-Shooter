using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float moveSpeed = 2f;

    private Vector3 tempScale;

    [SerializeField]
    private float stoppingDistance = 1.5f;

    private PlayerAnimation enemyAnimation;

    private void Awake()
    {
        playerTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        enemyAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        SearchForPlayer();
    }

    void SearchForPlayer()
    {
        if (!playerTarget)
            return;

        if (Vector3.Distance(transform.position, playerTarget.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.position, moveSpeed = Time.deltaTime);

            enemyAnimation.PlayAnimation(TagManager.WALK_ANIMATION_NAME);
            HandleFacingDirection()
        }


    }

    void HandleFacingDirection()
    {
        tempScale = transform.localScale;

        if (transform.position.x > playerTarget.position.x)
            tempScale.x = Mathf.Abs(tempScale.x);
        else
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;
    }
}
