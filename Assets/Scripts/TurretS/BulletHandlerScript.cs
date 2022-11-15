using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandlerScript : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 20;
    /*[SerializeField]
    private bool isCriticalHit = false;*/

    private void OnTriggerEnter(Collider other)
    {
        //Removes the bullet if it collides with something that's not a turret.
        if (other.tag != "Turret")
        {
            Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        //Moves the bullet forwards.
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }
}
