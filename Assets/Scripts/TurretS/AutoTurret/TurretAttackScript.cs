using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TurretAttackScript : MonoBehaviour
{
    [SerializeField]
    private Transform target, bulletPrefab, turretCannon;
    [SerializeField]
    private float maxDistance = 500;
    [SerializeField]
    private int shotCooldown = 2;
    private Stopwatch shotTimer = new Stopwatch();


    private void Start()
    {
        //Activates the timer for shooting targets as soon as the object is made.
        shotTimer.Start();
        //Finds the transform of the turrets cannon in it's children.
        turretCannon = transform.Find("Turret");
    }
    private void LookForTarget()
    {
        //This function Checks all enemies, looks to see if they are within range and if they are, chooses the closest one as a target.
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Enemy");
        int targetArrayPos = 0;
        float smallestDistanceFound = maxDistance;
        for (int i = 0; i < possibleTargets.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, possibleTargets[i].transform.position);
            if (distance < smallestDistanceFound)
            {
                targetArrayPos = i;
                smallestDistanceFound = distance;
            }

        }
        target = possibleTargets[targetArrayPos].transform;
    }

    private void ShootBullet()
    {
        //Create an instance of a bullet, set it's location to the turret and aim it at the target.
        var bullet = Instantiate(bulletPrefab);
        bullet.transform.position = turretCannon.position;
        bullet.transform.LookAt(target);

    }

    private void Update()
    {
        //If there is a target, aim at it and fire at a set interval. If there is no target, try to find a new one.
        if (target != null)
        {
            transform.LookAt((new Vector3(target.position.x, transform.position.y, target.position.z)));

            if (shotTimer.Elapsed.TotalSeconds >= shotCooldown)
            {
                ShootBullet();
                shotTimer.Restart();
            }
        }
        if (target == null)
        {
            LookForTarget();
        }
    }
}
