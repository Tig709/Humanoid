using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("enter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("exit");
        }
    }
}
