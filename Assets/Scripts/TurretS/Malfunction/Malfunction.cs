using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction : MonoBehaviour
{
    public List<GameObject> tasks;

    public GameObject CurrentTask;

    public int timer = 0;
    public bool timeractive = true;

    public TurretScript turret;
    public TurretAttackScript turretAtt;

    public Transform Cannon;

    // Update is called once per frame
    void Update()
    {
        if (timeractive)
        {
            timer++;
        }

        if (turret != null)
        {
            if (timer == 1500 && !turret.used && !turret.broken)
            {
                timeractive = false;
                timer = 0;
                Malfunctions();
            }
        }
        else
        {
            if (timer == 1500 && !turretAtt.broken)
            {
                timeractive = false;
                timer = 0;
                Malfunctions();
            }
        }
    }



    public void Malfunctions()
    {
        CurrentTask = tasks[Random.Range(0, tasks.Count)];
        Cannon.localRotation = Quaternion.AngleAxis(-60, Vector3.forward);
        if (turret != null)
        {
            turret.broken = true;
        }
        else
        {
            turretAtt.broken = true;
        }
    }
    public void FixedTurret()
    {
        Cannon.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        if (turret != null)
        {
            turret.broken = false;
        }
        else
        {
            turretAtt.broken = false;
        }
        timeractive = true;
        CurrentTask.SetActive(false);
    }

    public void FailedCheck()
    {
        CurrentTask.SetActive(false);
    }

}
