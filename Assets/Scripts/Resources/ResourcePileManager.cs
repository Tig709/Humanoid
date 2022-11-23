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

    //used to decide which number gets multiplied with the scale of the pile
    [SerializeField]
   private float scaleFactor = 1.5f;

    //this boolean checks if pilelevel is zero
    private bool isPileLevel0 = true;

    //used to not show the resource pile when it is level 0
    private Vector3 notActivePosition;
    private Vector3 activePosition;

    private GameObject Scrap;
    float timePassed = 0f;
    bool hasCollision = false;

    void Start()
    {
        Scrap = this.gameObject;
        notActivePosition = Scrap.transform.position;
        activePosition = new Vector3(Scrap.transform.position.x, 0.5f, Scrap.transform.position.z);
        StartCounting();
        Scrap.transform.position = notActivePosition;
    }

    private void Update()
    {

        if(hasCollision && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("hasColl");
        }
        if (!isPileLevel0 && pileLevel < maxPileLevel)
        {
            timePassed += Time.deltaTime;
            if (timePassed > timeBetweenNext)
            {
                pileLevel++;
                Scrap.transform.localScale *= scaleFactor;
                timePassed = 0;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" )//&& Input.GetKeyDown(KeyCode.E))
        {
           // Debug.Log("collision");
            hasCollision = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hasCollision = false;
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
            Scrap.transform.position = activePosition;
            isPileLevel0 = false;
        }
    }
}


