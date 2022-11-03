using UnityEngine;

public class PlaceBarrier : MonoBehaviour
{
    [SerializeField]
    private int barrierCost, barrierYOfsset, barrierBuildTime, barrierX1, barrierX2;

    [SerializeField]
    GameObject barrierPrefab;

    [SerializeField]
    GameObject barrierWallPrefab;

    private bool canBuild = true;

    [SerializeField]
    private Vector3 barrierPosition;

    private string whatToPlace = "PlaceFirst";

    private void Update()
    {
        myInput();
    }

    private void myInput()
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
        whatToPlace = "PlaceSecond";
    }

    private void PlaceSecond() 
    {
        barrierX2 = 5; //moet gridtile worden
        barrierPosition = new Vector3(barrierPosition.x + barrierX2, barrierPosition.y, barrierPosition.z);
        Instantiate(barrierPrefab, barrierPosition, Quaternion.identity);
        whatToPlace = "SecondDown";
    }

    private void SecondDown()
    {
        PlaceWall();
        whatToPlace = "PlaceFirst";
        ResetPlacement();
    }

    private void ResetPlacement() 
    {
        canBuild = true;
        barrierPosition.z = barrierPosition.z + 1;
    }

    private void PlaceWall() {
        GameObject[] barrierWall = GameObject.FindGameObjectsWithTag("BarrierTower");
        foreach(GameObject gameObject in barrierWall)
        {
            gameObject.gameObject.SetActive(false);
        }
        barrierPosition = new Vector3(barrierPosition.x -barrierX2, barrierPosition.y, barrierPosition.z);
        Instantiate(barrierWallPrefab, barrierPosition, Quaternion.identity);
    }
}
