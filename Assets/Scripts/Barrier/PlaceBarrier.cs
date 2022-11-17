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
        MyInput();
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
        barrierPosition = new Vector3(transform.position.x, barrierPosition.y, transform.position.z);
        //barrierPosition = new Vector3(barrierPosition.x + barrierX1, barrierPosition.y, barrierPosition.z);
        Instantiate(barrierPrefab, barrierPosition, Quaternion.identity);
        whatToPlace = "PlaceFirst";
    }

}
