using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePileManager : MonoBehaviour
{

    [SerializeField]
    private GameObject ScrapLayer1;
    [SerializeField]
    private GameObject ScrapLayer2;
    [SerializeField]
    private GameObject ScrapLayer3;
    [SerializeField]
    private GameObject ScrapLayer4;
    [SerializeField]
    private GameObject ScrapLayer5;

    [SerializeField]
    private int currentTime = 0;
    [SerializeField]
    private int startTime = 15;
    [SerializeField]
    private int timeBetweenNext = 1;
    [SerializeField]
    private int heapLevel = 0;

    private int starttime = 0;
    private bool heapLevel0 = true;



    // Start is called before the first frame update
    void Start()
    {
        StartCounting();
    }

    private void Update()
    {
        Debug.Log("Resource pile level is " + heapLevel);
        Debug.Log("Current time is " + currentTime);

        switch (heapLevel)
        {
            case 0:
                break;
            case 1: 
                Destroy(this.gameObject);
                Instantiate(ScrapLayer1, transform.position, Quaternion.identity);
                break;
            case 2:
                Destroy(this.gameObject);
                Instantiate(ScrapLayer2, transform.position, Quaternion.identity);
                break;
            case 3:
                Destroy(this.gameObject);
                Instantiate(ScrapLayer3, transform.position, Quaternion.identity);
                break;
            case 4:
                Destroy(this.gameObject);
                Instantiate(ScrapLayer4, transform.position, Quaternion.identity);
                break;
            case 5:
                Destroy(this.gameObject);
                Instantiate(ScrapLayer5, transform.position, Quaternion.identity);
                break;
        }
    }

    public void StartCounting()
    {

        InvokeRepeating("Count", 0, 1);

    }

    public void Count()
    {
        if (currentTime < startTime)
        {
            currentTime++;
        }

        if (currentTime == startTime && heapLevel0)
        {
            heapLevel = 1;
            heapLevel0 = false; 
            currentTime = 0;
        }
    }

    public void nextHeapLevel()
    {

        if (heapLevel0 && starttime < timeBetweenNext)
        {
            starttime++;
        }
        if (currentTime >= timeBetweenNext && !heapLevel0)
        {
            currentTime = 0;
            heapLevel++;
        }
    }
}

