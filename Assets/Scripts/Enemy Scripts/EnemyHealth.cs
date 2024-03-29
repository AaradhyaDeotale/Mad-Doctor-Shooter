
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Slider enemyHealthSlider; // Added Slider type

    private Enemy enemyScript;

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (health <= 0)
        {
            return;
        }
        health -= damageAmount;

        if (health <= 0f)
        {
            health = 0;

            // Kill enemy
            enemyScript.EnemyDied();

            EnemySpawner.instance.EnemyDied(gameObject);

            GamePlayController.instance.EnemyKilled();
        }

        // Assuming enemyHealthSlider is connected to a Slider component
        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.value = health;
        }
    }
}