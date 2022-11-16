using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction : MonoBehaviour
{
    public List<GameObject> tasks;

    public GameObject CurrentTask;

    public int timer = 0;
    public bool timeractive = true;

    TurretScript turret;

    // Start is called before the first frame update
    void Start()
    {
        turret = GetComponent<TurretScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeractive)
        {
            timer++;
        }


        if (timer == 1500 && !turret.used && !turret.broken)
        {
            timeractive = false;
            timer = 0;
            Malfunctions();
        }
    }



    public void Malfunctions()
    {
        CurrentTask = tasks[Random.Range(0, tasks.Count)];
        turret.Cannon.localRotation = Quaternion.AngleAxis(-60, Vector3.forward);
        turret.broken = true;
    }
    public void FixedTurret()
    {
        turret.Cannon.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        turret.broken = false;
        timeractive = true;

        CurrentTask.SetActive(false);

    }
}
