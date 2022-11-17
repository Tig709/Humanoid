using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierHealth : MonoBehaviour
{
    [SerializeField]
    int barrierHealth, barrierBaseHealth;

    [SerializeField]
    GameObject barriers;

    [SerializeField]
    BarrierHealthBar healthbar;

    [SerializeField]
    Canvas healthCanvas;

    [SerializeField]
    float showBarrierHealthTimer = 0f, showBarrierHealthTimerLimit = 2f;

    bool showHealthbar = false;

    [SerializeField]
    int incomingDamage;


    private void Start()
    {
        healthbar.SetMaxHealth(barrierBaseHealth);
        healthbar.SetHealth(barrierBaseHealth);
        healthCanvas.enabled = false;
    }
    public void TakeDamage(int damageAmount)
    {
        barrierHealth -= damageAmount;
        showHealthbar = true;
/*        if (barrierHealth <= 0)
        {
            GetDestroyed();
        }*/
        healthbar.SetHealth(barrierHealth);
    }

    public void RepairDamage(int repairAmount)
    {
        barrierHealth += repairAmount;
        if (barrierHealth > barrierBaseHealth)
        {
            barrierHealth = barrierBaseHealth;
        }
        healthbar.SetHealth(barrierHealth);
    }

    public void GetDestroyed()
    {
        Instantiate(barriers, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50);
        }

        if (showHealthbar)
        {
            healthCanvas.enabled = true;
            if (barrierHealth == barrierBaseHealth)
            {
                showBarrierHealthTimer += Time.deltaTime;
            }
            else
            {
                showBarrierHealthTimer = 0;
            }
        }
        else
        {
            healthCanvas.enabled = false;
        }

        if (showBarrierHealthTimer >= showBarrierHealthTimerLimit)
        {
            showHealthbar = false;
            showBarrierHealthTimer = 0;
        }
    }

    // Collision with enemy projectiles
    private void OnTriggerEnter(Collider other)
    {
        // If the collision with bullet take damage to the bulletDamage
        if (other.tag == "EnemyProjectile")
        {
            //TakeDamage(bullet.getBulletDamage());
            TakeDamage(incomingDamage);
        }
    }
}
