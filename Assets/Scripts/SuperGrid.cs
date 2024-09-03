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
    Shader gridCellActiveShader;

    [SerializeField]
    Color UNASSIGNED, ASSIGNED, INSPECTED, QC_ASSIGNED, COMPLETE;

    private static GameObject[,] _superGridCells;

    public int width = 1000;
    public int height = 100;

    public enum Actions { AddCell, RemoveCell };

    public Actions action;

    Dictionary<SuperGridCellProperties.SurveyStatus, Material> statusMaterials;
    Dictionary<SuperGridCellProperties.SurveyStatus, Material> statusActiveMaterials;

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
        ClearGrid();

        _superGridCells = new GameObject[width, height];

        GameObject prefabSGC = superGridCellPrefab;

        CreateMaterials();
        CreateActiveMaterials();

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

                var matDict = Random.Range(0, 100) > 98 ? statusActiveMaterials : statusMaterials;

                if (props.status == SuperGridCellProperties.SurveyStatus.UNASSIGNED) newCell.GetComponentInChildren<MeshRenderer>().material = matDict[SuperGridCellProperties.SurveyStatus.UNASSIGNED];
                if (props.status == SuperGridCellProperties.SurveyStatus.ASSIGNED) newCell.GetComponentInChildren<MeshRenderer>().material = matDict[SuperGridCellProperties.SurveyStatus.ASSIGNED];
                if (props.status == SuperGridCellProperties.SurveyStatus.INSPECTED) newCell.GetComponentInChildren<MeshRenderer>().material = matDict[SuperGridCellProperties.SurveyStatus.INSPECTED];
                if (props.status == SuperGridCellProperties.SurveyStatus.QC_ASSIGNED) newCell.GetComponentInChildren<MeshRenderer>().material = matDict[SuperGridCellProperties.SurveyStatus.QC_ASSIGNED];
                if (props.status == SuperGridCellProperties.SurveyStatus.COMPLETE) newCell.GetComponentInChildren<MeshRenderer>().material = matDict[SuperGridCellProperties.SurveyStatus.COMPLETE];

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

        foreach (var child in GetComponentsInChildren<SuperGridCell>())
        { DestroyImmediate(child.gameObject); }

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

        statusMaterials[SuperGridCellProperties.SurveyStatus.UNASSIGNED].SetColor("_Color", UNASSIGNED);
        statusMaterials[SuperGridCellProperties.SurveyStatus.ASSIGNED].SetColor("_Color", ASSIGNED);
        statusMaterials[SuperGridCellProperties.SurveyStatus.INSPECTED].SetColor("_Color", INSPECTED);
        statusMaterials[SuperGridCellProperties.SurveyStatus.QC_ASSIGNED].SetColor("_Color", QC_ASSIGNED);
        statusMaterials[SuperGridCellProperties.SurveyStatus.COMPLETE].SetColor("_Color", COMPLETE);
    }
    void CreateActiveMaterials()
    {
        statusActiveMaterials = new()
        {
            [SuperGridCellProperties.SurveyStatus.UNASSIGNED] = new Material(gridCellActiveShader) { color = UNASSIGNED, name = "UNASSIGNED ACTIVE" },
            [SuperGridCellProperties.SurveyStatus.ASSIGNED] = new Material(gridCellActiveShader) { color = ASSIGNED, name = "ASSIGNED ACTIVE" },
            [SuperGridCellProperties.SurveyStatus.INSPECTED] = new Material(gridCellActiveShader) { color = INSPECTED, name = "INSPECTED ACTIVE" },
            [SuperGridCellProperties.SurveyStatus.QC_ASSIGNED] = new Material(gridCellActiveShader) { color = QC_ASSIGNED, name = "QC_ASSIGNED ACTIVE" },
            [SuperGridCellProperties.SurveyStatus.COMPLETE] = new Material(gridCellActiveShader) { color = COMPLETE, name = "COMPLETE ACTIVE" }
        };

        statusActiveMaterials[SuperGridCellProperties.SurveyStatus.UNASSIGNED].SetColor("_Color", UNASSIGNED);
        statusActiveMaterials[SuperGridCellProperties.SurveyStatus.ASSIGNED].SetColor("_Color", ASSIGNED);
        statusActiveMaterials[SuperGridCellProperties.SurveyStatus.INSPECTED].SetColor("_Color", INSPECTED);
        statusActiveMaterials[SuperGridCellProperties.SurveyStatus.QC_ASSIGNED].SetColor("_Color", QC_ASSIGNED);
        statusActiveMaterials[SuperGridCellProperties.SurveyStatus.COMPLETE].SetColor("_Color", COMPLETE);
    }
}
