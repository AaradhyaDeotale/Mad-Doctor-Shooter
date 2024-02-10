using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Button attackButton;
    public PlayerShootingManager playerShootingManager; // Update the reference type to PlayerShootingManager
    private bool isAttacking;
    private bool hasShot;


    void Awake()
    {
        // Ensure you've assigned the playerShootingManager script in the Unity Editor
        playerShootingManager = FindObjectOfType<PlayerShootingManager>();
        if (playerShootingManager == null)
            Debug.LogError("PlayerShootingManager reference not found in Awake.");

        attackButton.onClick.AddListener(OnAttackButtonClick);
        attackButton.onClick.AddListener(OnAttackButtonRelease); // Add a listener for button release

    }

    public bool IsAttacking()
    {
        return isAttacking;

    }

    public void OnAttackButtonClick()
    {
        Debug.Log("AttackButton Clicked");
        if (playerShootingManager != null)
        {
            // Only start attacking if not already attacking and hasn't shot yet
            if (!isAttacking && !hasShot)
            {
                playerShootingManager.SetIsAttacking(true);
                isAttacking = true;
                hasShot = true; // Set hasShot to true after shooting
            }
        }
        else
        {
            Debug.LogError("PlayerShootingManager reference not assigned in AttackButton script.");
        }
    }
    public void OnAttackButtonRelease()
    {
        Debug.Log("AttackButton Released");
        if (playerShootingManager != null)
        {
            // Stop attacking when the button is released
            playerShootingManager.SetIsAttacking(false);
            isAttacking = false;
            hasShot = false; // Reset hasShot flag when button is released

        }
    }
}
