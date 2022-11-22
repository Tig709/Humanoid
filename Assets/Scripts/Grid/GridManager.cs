using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public LayerMask layerMask;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    public int width, height;

    [SerializeField]
    Material pathMaterial;
    [SerializeField]
    Material barrierMaterial;
    [SerializeField]
    Material turretMaterial;

    [SerializeField]
    GameObject gridTilePrefab;

    private List<GameObject> gridTiles = new List<GameObject>();
    public List<GameObject> pathList = new List<GameObject>();

    private List<Vector2> pathTiles = new List<Vector2>();
    private List<Vector2> BarrierTiles = new List<Vector2>();
    private List<Vector2> TurretTiles = new List<Vector2>();


    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void Start()
    {
        MakePathlist();
        MakeTurretList();

        CreateGrid();
    }

    private void CreateGrid()
    {
        for (float x = 0; x < width * size; x ++)
        {
            for (float z = 0; z < height * size; z ++)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                var gridTile = Instantiate(gridTilePrefab, point, Quaternion.identity);
                gridTile.transform.parent = transform;
                gridTile.name = "Tile " + (x / size) + " , " + (z / size);
                var tileScale = new Vector3(size, 0.2f, size);
                gridTile.transform.localScale = tileScale;
                gridTile.layer = 6;
                gridTiles.Add(gridTile);
                gridTile.GetComponent<TileScript>().Index = new Vector2(x/size, z/size);
            }
        }
        CreatePath();
        createTurretSpots();

        foreach (var gridtile in gridTiles)
        {
            if (gridtile.tag == "Untagged")
            {
                Destroy(gridtile);
            }
        }
    }

    private void CreatePath()
    {

        foreach (var gridtile in gridTiles)
        {
            foreach (var pathtile in pathTiles)
            {
                if (gridtile.GetComponent<TileScript>().Index.x == pathtile.x && gridtile.GetComponent<TileScript>().Index.y == pathtile.y)
                {
                    gridtile.GetComponent<MeshRenderer>().material = pathMaterial;
                    gridtile.tag = "Path";
                    gridtile.GetComponent<TileScript>().isPath = true;

                    pathList.Add(gridtile);
                }
            }
        }

    }

    private void createTurretSpots()
    {

        foreach (var gridtile in gridTiles)
        {
            foreach (var barriertile in BarrierTiles)
            {
                if (gridtile.GetComponent<TileScript>().Index.x == barriertile.x && gridtile.GetComponent<TileScript>().Index.y == barriertile.y)
                {
                    gridtile.GetComponent<MeshRenderer>().material = barrierMaterial;
                    gridtile.tag = "BarrierPlace";
                }
            }
            foreach (var turrettile in TurretTiles)
            {
                if (gridtile.GetComponent<TileScript>().Index.x == turrettile.x && gridtile.GetComponent<TileScript>().Index.y == turrettile.y)
                {
                    gridtile.GetComponent<MeshRenderer>().material = turretMaterial;
                    gridtile.tag = "TurretPlace";
                }
            }
        }
    }

    public GameObject getClosestPathTile(Vector3 position)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = position;
        foreach (GameObject pathtile in pathList)
        {
            float dist = Vector3.Distance(pathtile.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = pathtile;
                minDist = dist;
            }
        }
        return tMin;

    }

    public bool CheckValidPosition(TurretHandler.TurretTypes type, Vector3 position)
    {
        RaycastHit TileData;

        Physics.Raycast(position, Vector3.down, out TileData, 5, layerMask);

        if (type == TurretHandler.TurretTypes.Manual || type == TurretHandler.TurretTypes.Automatic)
        {
            if (TileData.transform.gameObject.tag == "TurretPlace" && !TileData.transform.gameObject.GetComponent<TileScript>().Used)
            {
                TileData.transform.gameObject.GetComponent<TileScript>().Used = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (type == TurretHandler.TurretTypes.Barier)
        {
            if (TileData.transform.gameObject.tag == "BarrierPlace" && !TileData.transform.gameObject.GetComponent<TileScript>().Used)
            {
                TileData.transform.gameObject.GetComponent<TileScript>().Used = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void MakePathlist()
    {
        pathTiles.Add(new Vector2(1, 0));
        pathTiles.Add(new Vector2(1, 1));
        pathTiles.Add(new Vector2(1, 2));
        pathTiles.Add(new Vector2(1, 3));
        pathTiles.Add(new Vector2(1, 4));

        pathTiles.Add(new Vector2(2, 4));
        pathTiles.Add(new Vector2(3, 4));
        pathTiles.Add(new Vector2(4, 4));
        pathTiles.Add(new Vector2(5, 4));
        pathTiles.Add(new Vector2(6, 4));
        pathTiles.Add(new Vector2(7, 4));
        pathTiles.Add(new Vector2(8, 4));
        pathTiles.Add(new Vector2(9, 4));
        pathTiles.Add(new Vector2(10, 4));
        pathTiles.Add(new Vector2(11, 4));
        pathTiles.Add(new Vector2(12, 4));
        pathTiles.Add(new Vector2(13, 4));
        pathTiles.Add(new Vector2(14, 4));
        pathTiles.Add(new Vector2(15, 4));

        pathTiles.Add(new Vector2(15, 5));
        pathTiles.Add(new Vector2(15, 6));
        pathTiles.Add(new Vector2(15, 7));
        pathTiles.Add(new Vector2(15, 8));
        pathTiles.Add(new Vector2(15, 9));
        pathTiles.Add(new Vector2(15, 10));
        pathTiles.Add(new Vector2(15, 11));
        pathTiles.Add(new Vector2(15, 12));
        pathTiles.Add(new Vector2(15, 13));
        pathTiles.Add(new Vector2(15, 14));
        pathTiles.Add(new Vector2(15, 15));

        pathTiles.Add(new Vector2(14, 15));
        pathTiles.Add(new Vector2(13, 15));
        pathTiles.Add(new Vector2(12, 15));
        pathTiles.Add(new Vector2(11, 15));
        pathTiles.Add(new Vector2(10, 15));
        pathTiles.Add(new Vector2(9, 15));
        pathTiles.Add(new Vector2(8, 15));

        pathTiles.Add(new Vector2(8, 16));
        pathTiles.Add(new Vector2(8, 17));
        pathTiles.Add(new Vector2(8, 18));
        pathTiles.Add(new Vector2(8, 19));

        pathTiles.Add(new Vector2(8, 20));
        pathTiles.Add(new Vector2(8, 21));
        pathTiles.Add(new Vector2(8, 22));
        pathTiles.Add(new Vector2(8, 23));
        pathTiles.Add(new Vector2(8, 24));

        pathTiles.Add(new Vector2(8, 24));
        pathTiles.Add(new Vector2(7, 24));
        pathTiles.Add(new Vector2(6, 24));
        pathTiles.Add(new Vector2(5, 24));
        pathTiles.Add(new Vector2(4, 24));
        pathTiles.Add(new Vector2(3, 24));

        pathTiles.Add(new Vector2(3, 25));
        pathTiles.Add(new Vector2(3, 26));
        pathTiles.Add(new Vector2(3, 27));
        pathTiles.Add(new Vector2(3, 28));
        pathTiles.Add(new Vector2(3, 29));

        pathTiles.Add(new Vector2(16, 9));
        //Sector 3 ^
        pathTiles.Add(new Vector2(17, 9));
        pathTiles.Add(new Vector2(18, 9));
        pathTiles.Add(new Vector2(19, 9));
        pathTiles.Add(new Vector2(20, 9));
        pathTiles.Add(new Vector2(21, 9));

        pathTiles.Add(new Vector2(21, 10));
        pathTiles.Add(new Vector2(21, 11));
        pathTiles.Add(new Vector2(21, 12));
        pathTiles.Add(new Vector2(21, 13));
        pathTiles.Add(new Vector2(21, 14));
        pathTiles.Add(new Vector2(21, 15));
        pathTiles.Add(new Vector2(21, 16));
        pathTiles.Add(new Vector2(21, 17));
        pathTiles.Add(new Vector2(21, 18));
        pathTiles.Add(new Vector2(21, 19));
        pathTiles.Add(new Vector2(21, 20));

        pathTiles.Add(new Vector2(20, 20));
        pathTiles.Add(new Vector2(19, 20));
        pathTiles.Add(new Vector2(18, 20));
        pathTiles.Add(new Vector2(17, 20));

        pathTiles.Add(new Vector2(17, 21));
        pathTiles.Add(new Vector2(17, 22));
        pathTiles.Add(new Vector2(17, 23));
        pathTiles.Add(new Vector2(17, 24));
        pathTiles.Add(new Vector2(17, 25));
        pathTiles.Add(new Vector2(17, 26));

        pathTiles.Add(new Vector2(18, 26));
        pathTiles.Add(new Vector2(19, 26));
        pathTiles.Add(new Vector2(20, 26));
        pathTiles.Add(new Vector2(21, 26));
        pathTiles.Add(new Vector2(22, 26));
        pathTiles.Add(new Vector2(23, 26));
        pathTiles.Add(new Vector2(24, 26));
        pathTiles.Add(new Vector2(25, 26));
        pathTiles.Add(new Vector2(26, 26));
        pathTiles.Add(new Vector2(27, 26));
        pathTiles.Add(new Vector2(28, 26));
        pathTiles.Add(new Vector2(29, 26));
        pathTiles.Add(new Vector2(30, 26));
        pathTiles.Add(new Vector2(31, 26));
        pathTiles.Add(new Vector2(32, 26));

        pathTiles.Add(new Vector2(32, 25));
        pathTiles.Add(new Vector2(32, 24));
        pathTiles.Add(new Vector2(32, 23));
        pathTiles.Add(new Vector2(32, 22));
        pathTiles.Add(new Vector2(32, 21));
        pathTiles.Add(new Vector2(32, 20));
        pathTiles.Add(new Vector2(32, 19));
        pathTiles.Add(new Vector2(32, 18));

        pathTiles.Add(new Vector2(33, 18));
        pathTiles.Add(new Vector2(34, 18));

        pathTiles.Add(new Vector2(34, 17));
        pathTiles.Add(new Vector2(34, 16));
        pathTiles.Add(new Vector2(34, 15));
        pathTiles.Add(new Vector2(34, 14));
        pathTiles.Add(new Vector2(34, 13));
        pathTiles.Add(new Vector2(34, 12));

        pathTiles.Add(new Vector2(35, 12));
        //Sector 2 ^
        pathTiles.Add(new Vector2(36, 12));

        pathTiles.Add(new Vector2(37, 12));
        pathTiles.Add(new Vector2(38, 12));
        pathTiles.Add(new Vector2(39, 12));

        pathTiles.Add(new Vector2(39, 11));
        pathTiles.Add(new Vector2(39, 10));
        pathTiles.Add(new Vector2(39, 9));
        pathTiles.Add(new Vector2(39, 8));
        pathTiles.Add(new Vector2(39, 7));
        pathTiles.Add(new Vector2(39, 6));
        pathTiles.Add(new Vector2(39, 5));
        pathTiles.Add(new Vector2(39, 4));

        pathTiles.Add(new Vector2(40, 4));
        pathTiles.Add(new Vector2(41, 4));
        pathTiles.Add(new Vector2(42, 4));
        pathTiles.Add(new Vector2(43, 4));
        pathTiles.Add(new Vector2(44, 4));
        pathTiles.Add(new Vector2(45, 4));
        pathTiles.Add(new Vector2(46, 4));
        pathTiles.Add(new Vector2(47, 4));

        pathTiles.Add(new Vector2(47, 5));
        pathTiles.Add(new Vector2(47, 6));
        pathTiles.Add(new Vector2(47, 7));
        pathTiles.Add(new Vector2(47, 8));
        pathTiles.Add(new Vector2(47, 9));
        pathTiles.Add(new Vector2(47, 10));
        pathTiles.Add(new Vector2(47, 11));
        pathTiles.Add(new Vector2(47, 12));
        pathTiles.Add(new Vector2(47, 13));
        pathTiles.Add(new Vector2(47, 14));
        pathTiles.Add(new Vector2(47, 15));
        pathTiles.Add(new Vector2(47, 16));
        pathTiles.Add(new Vector2(47, 17));
        pathTiles.Add(new Vector2(47, 18));

        pathTiles.Add(new Vector2(46, 18));
        pathTiles.Add(new Vector2(45, 18));
        pathTiles.Add(new Vector2(44, 18));
        pathTiles.Add(new Vector2(43, 18));

        pathTiles.Add(new Vector2(43, 19));
        pathTiles.Add(new Vector2(43, 20));
        pathTiles.Add(new Vector2(43, 21));
        pathTiles.Add(new Vector2(43, 22));
        pathTiles.Add(new Vector2(43, 23));
        pathTiles.Add(new Vector2(43, 24));

        pathTiles.Add(new Vector2(44, 24));
        pathTiles.Add(new Vector2(45, 24));
        pathTiles.Add(new Vector2(46, 24));

        pathTiles.Add(new Vector2(46, 25));
        pathTiles.Add(new Vector2(46, 26));
        pathTiles.Add(new Vector2(46, 27));
        pathTiles.Add(new Vector2(46, 28));
        pathTiles.Add(new Vector2(46, 29));

        //Sector 1 ^

    }
    private void MakeTurretList()
    {
        // barrier turret
        BarrierTiles.Add(new Vector2(45, 27));
        BarrierTiles.Add(new Vector2(47, 27));

        BarrierTiles.Add(new Vector2(42, 20));
        BarrierTiles.Add(new Vector2(44, 20));

        BarrierTiles.Add(new Vector2(46, 7));
        BarrierTiles.Add(new Vector2(48, 7));

        BarrierTiles.Add(new Vector2(38, 6));
        BarrierTiles.Add(new Vector2(40, 6));

        BarrierTiles.Add(new Vector2(29, 27));
        BarrierTiles.Add(new Vector2(29, 25));

        BarrierTiles.Add(new Vector2(20, 16));
        BarrierTiles.Add(new Vector2(22, 16));

        BarrierTiles.Add(new Vector2(7, 19));
        BarrierTiles.Add(new Vector2(9, 19));

        BarrierTiles.Add(new Vector2(9, 5));
        BarrierTiles.Add(new Vector2(9, 3));


        // Automatic and manual turret
        TurretTiles.Add(new Vector2(45, 29));
        TurretTiles.Add(new Vector2(47, 29));
        TurretTiles.Add(new Vector2(43, 25));
        TurretTiles.Add(new Vector2(47, 25));
        TurretTiles.Add(new Vector2(46, 23));
        TurretTiles.Add(new Vector2(43, 25));
        TurretTiles.Add(new Vector2(42, 22));
        TurretTiles.Add(new Vector2(47, 19));
        TurretTiles.Add(new Vector2(45, 17));
        TurretTiles.Add(new Vector2(48, 15));
        TurretTiles.Add(new Vector2(46, 12));
        TurretTiles.Add(new Vector2(48, 10));
        TurretTiles.Add(new Vector2(46, 8));
        TurretTiles.Add(new Vector2(46, 5));
        TurretTiles.Add(new Vector2(48, 3));
        TurretTiles.Add(new Vector2(44, 3));
        TurretTiles.Add(new Vector2(40, 3));
        TurretTiles.Add(new Vector2(38, 4));
        TurretTiles.Add(new Vector2(38, 9));
        TurretTiles.Add(new Vector2(40, 10));
        TurretTiles.Add(new Vector2(39, 13));
        TurretTiles.Add(new Vector2(37, 11));
        TurretTiles.Add(new Vector2(33, 13));
        TurretTiles.Add(new Vector2(35, 15));
        TurretTiles.Add(new Vector2(35, 18));
        TurretTiles.Add(new Vector2(32, 17));
        TurretTiles.Add(new Vector2(33, 20));
        TurretTiles.Add(new Vector2(31, 22));
        TurretTiles.Add(new Vector2(33, 26));
        TurretTiles.Add(new Vector2(31, 27));
        TurretTiles.Add(new Vector2(25, 27));
        TurretTiles.Add(new Vector2(23, 25));
        TurretTiles.Add(new Vector2(27, 25));
        TurretTiles.Add(new Vector2(19, 25));
        TurretTiles.Add(new Vector2(17, 27));
        TurretTiles.Add(new Vector2(16, 26));
        TurretTiles.Add(new Vector2(16, 22));
        TurretTiles.Add(new Vector2(19, 19));
        TurretTiles.Add(new Vector2(21, 21));
        TurretTiles.Add(new Vector2(22, 18));
        TurretTiles.Add(new Vector2(20, 14));
        TurretTiles.Add(new Vector2(22, 12));
        TurretTiles.Add(new Vector2(20, 10));
        TurretTiles.Add(new Vector2(19, 8));
        TurretTiles.Add(new Vector2(5, 23));
        TurretTiles.Add(new Vector2(9, 23));
        TurretTiles.Add(new Vector2(7, 17));
        TurretTiles.Add(new Vector2(9, 17));
        TurretTiles.Add(new Vector2(12, 16));
        TurretTiles.Add(new Vector2(10, 14));
        TurretTiles.Add(new Vector2(16, 14));
        TurretTiles.Add(new Vector2(14, 12));
        TurretTiles.Add(new Vector2(16, 7));
        TurretTiles.Add(new Vector2(14, 5));
        TurretTiles.Add(new Vector2(12, 3));
        TurretTiles.Add(new Vector2(6, 5));
        TurretTiles.Add(new Vector2(3, 3));
    }
}