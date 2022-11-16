using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    [SerializeField] public int maxHealth = 200;
    [SerializeField] public int currentHealth;

    public BarrierHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            TakeDamage(20);
        }
    }
    /*public void OnTrigger{ 
    {
        if ()
        {
            TakeDamage(20);
        }
    }*/

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("Spawner") != null)
        {
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().spawnedEnemies.Remove(gameObject);
        }

    }
}
