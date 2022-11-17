using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Vector3 basePos;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private int bulletDamage;

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    // Collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Enemy" && other.tag != "EnemyProjectile")
        {
            //transform.position = basePos;
            Destroy(this.gameObject);
        }
    }

    // Getter for the bulletDamage
    public int getBulletDamage()
    {
        return bulletDamage;
    }
}