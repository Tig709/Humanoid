using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePileManager : MonoBehaviour
{
    //current time used to count the cooldown
    [SerializeField]//serialized for debugging
    private int currentTime = 0;

    //cooldown is used to leave a resource spot empty after all resources are taken and when game starts
    [SerializeField]
    private int cooldown = 5;

    //time between next is used to determine the time between the next upgrade of size for the resource pile
    [SerializeField]
    private int timeBetweenNext = 3;

    //level is the amount of resources you get from the spot
    [SerializeField]//serialized for debugging
    private int pileLevel = 0;

    //size when pile stops growing
    [SerializeField]
    private int maxPileLevel = 5;

    //this boolean checks if pilelevel is zero
    private bool isPileLevel0 = true;

    private GameObject Scrap;
    float timePassed = 0f;
    
    void Start()
    {
        Scrap = this.gameObject;
        StartCounting();
    }

    private void Update()
    {
        Debug.Log(pileLevel);
        if (!isPileLevel0 && pileLevel <= maxPileLevel)
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeBetweenNext)
            {
                pileLevel++;
                Scrap.transform.localScale *= 1.2f;
                timePassed = 0;
            }
        }
    }

    public void StartCounting()
    {
        if (isPileLevel0)
        {
            InvokeRepeating("Count", 0, 1);
        }
    }

    public void Count()
    {
        if (currentTime < cooldown)
        {
            currentTime++;
        }

        if (currentTime == cooldown && isPileLevel0)
        {
            pileLevel++;
            Scrap.transform.localScale *= 1.2f;
            isPileLevel0 = false;
        }
    }
}


