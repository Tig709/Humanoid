using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Get the Bullet for the BulletDamage
    //private Bullet bullet;

    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;

    [SerializeField] public EnemyAIHealthBar healthBar;

    [SerializeField] private int incomingDamage = 20;

    private void Start()
    {
        // Initialize bullet
        //bullet = FindObjectOfType<Bullet>();

        // Set the currenthealth
        currentHealth = maxHealth;

        // Set the healthbar to the maxHealth
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collision with bullet take damage to the bulletDamage
        if (other.tag == "TurretProjectile")
        {
            //TakeDamage(bullet.getBulletDamage());
            TakeDamage(incomingDamage);
        }
    }

    private void TakeDamage(int damage)
    {
        // Take the damage
        currentHealth -= damage;

        // Set the healthbar to the currentHealth
        healthBar.SetHealth(currentHealth);

        // Check if the enemy is dead
        checkIfDead();
    }

    private void checkIfDead()
    {
        // If the currentHealth is 0 or lower kill the enemy
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
