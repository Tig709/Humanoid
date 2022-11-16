using UnityEngine;
using System.Collections;

public class PlaceBarrier : MonoBehaviour
{
    [SerializeField]
    private int barrierCost, barrierYOfsset, barrierBuildTime, barrierX1, barrierX2;

    [SerializeField]
    GameObject barrierPrefab;

    [SerializeField]
    GameObject barrierWallPrefab;

    private GameObject[] otherBarriers;
    private bool canBuild = true;
    float distance = Mathf.Infinity;
    GameObject closest = null;

    [SerializeField]
    private Vector3 barrierPosition;
    private Vector3[] barrierPositions;
    

    private string whatToPlace = "PlaceFirst";

    private void Update()
    {
        FindClosestTower();
        MyInput();
    }

        public GameObject FindClosestTower()
        {

            otherBarriers = GameObject.FindGameObjectsWithTag("BarrierTower");
            

            Vector3 position = barrierPosition;
            foreach (GameObject ob in otherBarriers)
            {
                Vector3 diff = ob.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = ob;
                    distance = curDistance;
                }
            }
            return closest;
        }
    
    private void MyInput()
    {
        if (Input.GetKeyDown(KeyCode.F) && canBuild)
        {
          Invoke(whatToPlace, 0f);
            canBuild = false;
        }

        if (Input.GetKeyUp(KeyCode.F) )
        {
            canBuild = true; 
        }
    }

    private void PlaceFirst()
    {
        barrierX1 = 0; //moet gridtile worden
        barrierPosition = new Vector3(transform.position.x, barrierPosition.y, transform.position.z);
        //barrierPosition = new Vector3(barrierPosition.x + barrierX1, barrierPosition.y, barrierPosition.z);
        Instantiate(barrierPrefab, barrierPosition, Quaternion.identity);
        whatToPlace = "PlaceFirst";
        if (distance < 20)
        {
            PlaceWall(closest.transform.position);
        }

    }

    private void PlaceWall(Vector3 closestPos) {
        GameObject[] barrierWall = GameObject.FindGameObjectsWithTag("BarrierTower");
        foreach(GameObject gameObject in barrierWall)
        {
            gameObject.gameObject.SetActive(false);
        }
        //barrierPosition = new Vector3(barrierPosition.x -barrierX2, barrierPosition.y, barrierPosition.z);
        Instantiate(barrierWallPrefab, closestPos, Quaternion.identity);
    }
}
