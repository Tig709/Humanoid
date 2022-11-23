using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField]  int Health;

    //public BarrierHealthBar healthBar;

    void Start()
    {
        
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
        Health -= damage;

        if (Health <= 0)
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
