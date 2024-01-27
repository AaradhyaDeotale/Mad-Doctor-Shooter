using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    public Button attackButton;
    public PlayerShootingManager playerShootingManager; // Update the reference type to PlayerShootingManager
    private bool isAttacking;

    void Awake()
    {
        // Ensure you've assigned the playerShootingManager script in the Unity Editor
        playerShootingManager = FindObjectOfType<PlayerShootingManager>();
        if (playerShootingManager == null)
            Debug.LogError("PlayerShootingManager reference not found in Awake.");

        attackButton.onClick.AddListener(OnAttackButtonClick);
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    void OnAttackButtonClick()
    {
        Debug.Log("AttackButton Clicked");
        if (playerShootingManager != null)
        {
            playerShootingManager.SetIsAttacking(true);
            isAttacking = true;
        }
        else
        {
            Debug.LogError("PlayerShootingManager reference not assigned in AttackButton script.");
        }
    }

    public void OnAttackButtonRelease()
    {
        if (playerShootingManager != null)
        {
            playerShootingManager.SetIsAttacking(false);
            isAttacking = false;
        }
    }
}
