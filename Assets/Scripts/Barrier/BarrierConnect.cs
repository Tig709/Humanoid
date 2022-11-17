using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierConnect : MonoBehaviour
{
    private GameObject[] otherBarriers;
    float distance;
    GameObject closest = null;
    [SerializeField]
    GameObject barrierWallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        distance = 400;
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTower();
        WallRotation();
    }

    public GameObject FindClosestTower()
    {

        otherBarriers = GameObject.FindGameObjectsWithTag("BarrierTower");

        foreach (GameObject ob in otherBarriers)
        {
            float curDistance = Vector3.Distance(transform.position, ob.transform.position);
            if (curDistance < distance)
            {
                closest = ob;
                distance = curDistance;
                Vector2 direction = transform.position - ob.transform.position;
                direction.Normalize();
                Debug.Log(distance);
                if (distance < 10&& distance>5)
                {
                    GameObject[] barrierWall = GameObject.FindGameObjectsWithTag("BarrierTower");
                    foreach (GameObject gameObject in barrierWall)
                    {
                        ob.SetActive(false);
                        this.gameObject.SetActive(false);
                    }
                    Instantiate(barrierWallPrefab, transform.position, Quaternion.identity);
                }
            }
        }
        return closest;
    }

    public void WallRotation()
    {

    }

}
