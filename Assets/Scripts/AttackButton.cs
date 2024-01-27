using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Button attackButton;
    public PlayerMovement playerMovement;

    void Start()
    {
        // Ensure you've assigned the playerMovement script in the Unity Editor
        playerMovement = GetComponent<PlayerMovement>();
        attackButton.onClick.AddListener(OnAttackButtonClick);
    }

    void FindPlayer()
    {
        // Find the Player GameObject by its tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG);

        if (playerObject != null)
        {
            // Get the PlayerMovement component from the Player GameObject
            playerMovement = playerObject.GetComponent<PlayerMovement>();

            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement component not found on the Player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }
    void OnAttackButtonClick()
    {
        if (playerMovement != null)
        {
            playerMovement.HandleAttackButton();
        }
        else
        {
            Debug.LogError("PlayerMovement reference not assigned in AttackButton script.");
        }
    }
}