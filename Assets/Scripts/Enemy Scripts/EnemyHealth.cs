using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;

<<<<<<< HEAD
    [SerializeField] private Slider enemyHealthSlider;
=======
    [SerializeField] private SliderJoint2D enemyHealthSlider;
>>>>>>> 9e9842a89126177b317c1ae66bf8a872566e44db

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
    