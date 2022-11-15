using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBillBoardScript : MonoBehaviour
{
    [SerializeField]
    Transform cam;
    Transform myPos;

    private void Start()
    {
        myPos = GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        transform.LookAt(myPos.position + cam.forward);
    }
}
