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
        cam = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.LookAt(myPos.position + cam.forward);
    }
}
