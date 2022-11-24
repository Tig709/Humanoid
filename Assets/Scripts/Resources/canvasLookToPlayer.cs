using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasLookToPlayer : MonoBehaviour
{
    private Transform target;
    private GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }
    public void Update()
    {
        var lookpos = target.position - transform.position;
        lookpos.y = 0;
        var rotation = Quaternion.LookRotation(lookpos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime* 2f);
    }
}
