using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBarrier : MonoBehaviour
{
    [SerializeField]
    GameObject activeBarrier;

    [SerializeField]
    float repairTimer = 0f, repairTimeTarget = 0.5f;

    [SerializeField]
    int repairAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Barrier")
        {
            activeBarrier = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Barrier")
        {
            activeBarrier = null;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            repairTimer += Time.deltaTime;
        }
        else
        {
            repairTimer = 0;
        }

        if (repairTimer >= repairTimeTarget)
        {
            repairTimer = 0;
            ActivateTurretRepair();
        }
    }

    void ActivateTurretRepair()
    {
        if (activeBarrier != null)
        {
            Debug.Log("Turret repair attempted");
            activeBarrier.GetComponent<BarrierHealth>().RepairDamage(repairAmount);
        }
    }
}
