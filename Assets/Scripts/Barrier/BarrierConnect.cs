using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierConnect : MonoBehaviour
{
    TurretHandler TurretHandler;

    public GameObject WallPrefab;

    public int distance;

    public bool Found, WallMade;

    public GameObject OtherTowerFound;



    private void Start()
    {
        Found = false;
        WallMade = false;

        TurretHandler = FindObjectOfType<TurretHandler>();
    }


    private void Update()
    {
        if (!Found)
        {
            LookForTowers();
        }
    }

    private void LookForTowers()
    {
        foreach (GameObject Tower in TurretHandler.BarrierList)
        {
                if (Vector3.Distance(Tower.transform.position,this.transform.position) <= 10)
                { 
                if (Tower.gameObject == this.gameObject)
                {
                    continue;
                }
                else
                {
                    OtherTowerFound = Tower;
                    Found = true;
                }
            
            }
        }

    }

}
