using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBillboard : MonoBehaviour
{
    [SerializeField]
    Transform cam;
    Transform myPos;

    private void Start()
    {
        myPos = GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.LookAt(myPos.position + cam.forward);
    }
}