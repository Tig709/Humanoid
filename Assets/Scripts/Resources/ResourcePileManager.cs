using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePileManager : MonoBehaviour
{
    //TODO:
    //canvas must not scale
    //canvas only on screen when player close
    //canvas always look at player so player always can read text
    //other text color
    //player logic for pile

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
    public int pileLevel = 0;

    //size when pile stops growing
    [SerializeField]
    private int maxPileLevel = 5;

    //used to decide which number gets multiplied with the scale of the pile
    [SerializeField]
   private float scaleFactor = 1.5f;

    [SerializeField]
    private Canvas resourceCanvas;

    //this boolean checks if pilelevel is zero
    private bool isPileLevel0 = true;

    //used to not show the resource pile when it is level 0
    private Vector3 notActivePosition;
    private Vector3 activePosition;

    //decides startscale so it can be resetted
    private Vector3 startScale;

    private GameObject Scrap;
    
    float timePassed = 0f;
    bool hasCollision = false;

    void Start()
    {
        Scrap = this.gameObject;
        notActivePosition = Scrap.transform.position;
        startScale = Scrap.transform.localScale;
        activePosition = new Vector3(Scrap.transform.position.x, 0.5f, Scrap.transform.position.z);
        StartCounting();
        Scrap.transform.position = notActivePosition;
    }

    private void Update()
    {
        if(hasCollision && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("hasColl");
            Scrap.transform.position = notActivePosition;
            Scrap.transform.localScale = startScale;
            pileLevel = 0;
            isPileLevel0 = true;
            currentTime = 0;
            timePassed = 0;
        }

        if (isPileLevel0)
        {
            resourceCanvas.gameObject.SetActive(false);
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


    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("enter");
            hasCollision = true;
            resourceCanvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("exit");
            hasCollision = false;
            resourceCanvas.gameObject.SetActive(false);
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


