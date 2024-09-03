using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class SuperGrid : MonoBehaviour
{
    public GameObject superGridCellPrefab;

    [SerializeField]
    Shader gridCellShader;

    [SerializeField]
    Color UNASSIGNED, ASSIGNED, INSPECTED, QC_ASSIGNED, COMPLETE;

    private  GameObject[,] _superGridCells;

    public int width = 1000;
    public int height = 100;

    public enum Actions { AddCell, RemoveCell };

    public Actions action;

    Dictionary<SuperGridCellProperties.SurveyStatus, Material> statusMaterials;

    private void Awake()
    {
        if (statusMaterials == null) CreateMaterials();
    }

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

        if (statusMaterials == null || statusMaterials.Count == 0) CreateMaterials();

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                GameObject newCell = PrefabUtility.InstantiatePrefab(prefabSGC) as GameObject;
                newCell.transform.parent = this.transform;


                var props = ScriptableObject.CreateInstance<SuperGridCellProperties>();
                {
                    props.col = i;
                    props.row = j;
                    props.center = new Vector2(i, j);
                    props.zEstimate = 0f;
                    props.status = (SuperGridCellProperties.SurveyStatus)Random.Range(0, 5);
                };

                newCell.GetComponent<SuperGridCell>().Initialize(props);

                newCell.transform.localPosition = new Vector3(i, j, props.zEstimate);
                newCell.transform.localScale = Vector3.one * 0.9f;

                if (props.status == SuperGridCellProperties.SurveyStatus.UNASSIGNED) newCell.GetComponentInChildren<MeshRenderer>().material = statusMaterials[SuperGridCellProperties.SurveyStatus.UNASSIGNED];
                if (props.status == SuperGridCellProperties.SurveyStatus.ASSIGNED) newCell.GetComponentInChildren<MeshRenderer>().material = statusMaterials[SuperGridCellProperties.SurveyStatus.ASSIGNED];
                if (props.status == SuperGridCellProperties.SurveyStatus.INSPECTED) newCell.GetComponentInChildren<MeshRenderer>().material = statusMaterials[SuperGridCellProperties.SurveyStatus.INSPECTED];
                if (props.status == SuperGridCellProperties.SurveyStatus.QC_ASSIGNED) newCell.GetComponentInChildren<MeshRenderer>().material = statusMaterials[SuperGridCellProperties.SurveyStatus.QC_ASSIGNED];
                if (props.status == SuperGridCellProperties.SurveyStatus.COMPLETE) newCell.GetComponentInChildren<MeshRenderer>().material = statusMaterials[SuperGridCellProperties.SurveyStatus.COMPLETE];

                _superGridCells[i, j] = newCell;
            }
        }
    }

    public void ClearGrid()
    {
        if (_superGridCells != null)
        {
            foreach (var cell in _superGridCells) { DestroyImmediate(cell); }
        }

        _superGridCells = null;
    }

    void CreateMaterials()
    {
        statusMaterials = new()
        {
            [SuperGridCellProperties.SurveyStatus.UNASSIGNED] = new Material(gridCellShader) { color = UNASSIGNED, name = "UNASSIGNED" },
            [SuperGridCellProperties.SurveyStatus.ASSIGNED] = new Material(gridCellShader) { color = ASSIGNED, name = "ASSIGNED" },
            [SuperGridCellProperties.SurveyStatus.INSPECTED] = new Material(gridCellShader) { color = INSPECTED, name = "INSPECTED" },
            [SuperGridCellProperties.SurveyStatus.QC_ASSIGNED] = new Material(gridCellShader) { color = QC_ASSIGNED, name = "QC_ASSIGNED" },
            [SuperGridCellProperties.SurveyStatus.COMPLETE] = new Material(gridCellShader) { color = COMPLETE, name = "COMPLETE" }
        };
    }
}
