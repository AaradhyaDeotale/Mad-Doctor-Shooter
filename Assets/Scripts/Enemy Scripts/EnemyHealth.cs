using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    [SerializeField] private SliderJoint2D enemyHealthSlider;

    private Enemy enemyScript;

    private void Awake() {
        enemyScript = GetComponent<Enemy>();
    }


    public void TakeDamage(float damageAmount) {
        if(health <= 0) {
            return;
        }
        health -= damageAmount;

        if(health <= 0f) {
            health = 0;

            //Kill enemy
            enemyScript.EnemyDied();

            EnemySpawner.instance.EnemyDied(gameObject);

            GamePlayController.instance.EnemyKilled();

        }

        enemyHealthSlider.value = health;
    }


}
    