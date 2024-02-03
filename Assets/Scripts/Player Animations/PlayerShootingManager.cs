using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPos;

    [SerializeField]
    private float shootCooldown = 0.5f; // Adjust the cooldown time as needed

    private bool isAttacking;
    private float lastShootTime;

    void Update()
    {
        // Check if the player is attacking and if enough time has passed since the last shot
        if (isAttacking && Time.time > lastShootTime + shootCooldown)
        {
            // Call the Shoot method when attacking
            Shoot(transform.localScale.x);
            lastShootTime = Time.time; // Update the last shoot time
        }
    }

    public void SetIsAttacking(bool attacking)
    {
        // Set the isAttacking boolean based on the attack button state
        isAttacking = attacking;
    }

    public void Shoot(float facingDirection)
    {
        Debug.Log("Shooting!");
        GameObject newBullet = Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.identity);

        if (facingDirection < 0)
            newBullet.GetComponent<Bullet>().SetNegativeSpeed();

        SoundManager.instance.PlayShootSound();
    }
}
