using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SuperGrid : MonoBehaviour
{
    public GameObject superGridCellPrefab;

    private  GameObject[,] _superGridCells;

    public int width = 1000;
    public int height = 100;

    public enum Actions { AddCell, RemoveCell };

    public Actions action;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
        _superGridCells = new GameObject[width, height];

        GameObject prefabSGC = superGridCellPrefab;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                GameObject newCell = PrefabUtility.InstantiatePrefab(prefabSGC) as GameObject;
                newCell.transform.parent = this.transform;
                newCell.transform.localPosition = new Vector3(i, j, 0);
                newCell.name = "SuperGrid Cell " + i + "-" + j;

                _superGridCells[i, j] = newCell;
            }
        }
    }
}
