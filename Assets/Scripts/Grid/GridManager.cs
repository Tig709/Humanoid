using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    [SerializeField]
    public int width, height;

    [SerializeField]
    Material pathMaterial;

    [SerializeField]
    GameObject gridTilePrefab;

    private List<GameObject> gridTiles = new List<GameObject>();

    private List<Vector2> pathTiles = new List<Vector2>(); 

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
        CreateGrid();
        CreatePath();
    }

    private void CreateGrid()
    {
        for (float x = 0; x < width * size; x += size)
        {
            for (float z = 0; z < height * size; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                var gridTile = Instantiate(gridTilePrefab, point, Quaternion.identity);
                gridTile.transform.parent = transform;
                gridTile.name = "Tile " + (x / size) + " , " + (z / size);
                var tileScale = new Vector3(size, 0.2f, size);
                gridTile.transform.localScale = tileScale;
                gridTiles.Add(gridTile);
                gridTile.GetComponent<TileScript>().Index = new Vector2(x/size, z/size);

            }

        }
    }

    private void CreatePath()
    {
        pathTiles.Add(new Vector2(1, 0));

        foreach (var gridtile in gridTiles)
        {
            foreach (var pathtile in pathTiles)
            {
                if (gridtile.GetComponent<TileScript>().Index.x == pathtile.x && gridtile.GetComponent<TileScript>().Index.y == pathtile.y)
                {
                    gridtile.GetComponent<MeshRenderer>().material = pathMaterial;
                    gridtile.tag = "Path";
                    gridtile.GetComponent<TileScript>().isPath = true;
                }
            }
        }
        
    }
}