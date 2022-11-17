using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealthScript : MonoBehaviour
{
    [SerializeField]
    int turretHealth = 100, turretBaseHealth = 100;

    /*[SerializeField]
    GameObject brokenTurretPrefab;*/

    [SerializeField]
    HealthBarScript healthbar;

    [SerializeField]
    Canvas healthCanvas;

    [SerializeField]
    float showTurretHealthTimer = 0f, showTurretHealthTimerLimit = 2f;

    bool showHealthbar = false;


    private void Start()
    {
        healthbar.SetMaxHealth(turretBaseHealth);
        healthbar.SetHealth(turretBaseHealth);
        healthCanvas.enabled = false;
    }
    public void TakeDamage(int damageAmount)
    {
        turretHealth -= damageAmount;
        showHealthbar = true;
        if (turretHealth <= 0)
        {
            GetDestroyed();
        }
        healthbar.SetHealth(turretHealth);
    }

    public void RepairDamage(int repairAmount)
    {
        turretHealth += repairAmount;
        if (turretHealth > turretBaseHealth)
        {
            turretHealth = turretBaseHealth;
        }
        healthbar.SetHealth(turretHealth);
    }

    public void GetDestroyed()
    {
        /*Instantiate(brokenTurretPrefab, this.transform.position, this.transform.rotation);*/
        //Destroy(this.gameObject);
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
            if (turretHealth == turretBaseHealth)
            {
                showTurretHealthTimer += Time.deltaTime;
            }
            else
            {
                showTurretHealthTimer = 0;
            }
        }
        else
        {
            healthCanvas.enabled = false;
        }
        if (showTurretHealthTimer >= showTurretHealthTimerLimit)
        {
            showHealthbar = false;
            showTurretHealthTimer = 0;
        }
    }
}
